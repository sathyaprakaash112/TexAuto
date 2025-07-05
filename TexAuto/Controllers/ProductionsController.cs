using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TexAuto.Data;
using TexAuto.Models.Domain.Entries;

namespace TexAuto.Controllers
{
    public class ProductionsController : Controller
    {
        private readonly TexAutoContext _context;

        public ProductionsController(TexAutoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productions = _context.Productions
                .Include(p => p.Shift)
                .Include(p => p.Department)
                .Include(p => p.Machine)
                .Include(p => p.ProductIn)
                .Include(p => p.ProductOut);

            return View(await productions.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetMachineCalculationType(int machineId)
        {
            var machine = await _context.Machines.FirstOrDefaultAsync(m => m.Id == machineId);
            return Json(machine?.CalculationType ?? "Production"); // fallback
        }


        public async Task<IActionResult> Create()
        {
            var prodDate = DateTime.Today;
            var fyStart = new DateTime(prodDate.Month >= 4 ? prodDate.Year : prodDate.Year - 1, 4, 1);
            var fyEnd = fyStart.AddYears(1).AddDays(-1);

            var nextNumber = _context.Productions
    .AsEnumerable() // ⬅️ Forces EF to load data and filter in memory
    .Where(p =>
        p.ProductionDate.ToDateTime(TimeOnly.MinValue) >= fyStart &&
        p.ProductionDate.ToDateTime(TimeOnly.MinValue) <= fyEnd)
    .Select(p => (int?)p.ProductionNumber)
    .Max() ?? 0;


            var production = new Production
            {
                ProductionDate = DateOnly.FromDateTime(prodDate),
                ProductionNumber = nextNumber + 1
            };

            var lastProduction = await _context.Productions
                .OrderByDescending(p => p.ProductionDate)
                .ThenByDescending(p => p.Id)
                .FirstOrDefaultAsync();

            var model = new Production();

            if (lastProduction == null)
            {
                model.ProductionDate = DateOnly.FromDateTime(DateTime.Today);
                model.ShiftDetails = "Shift 1";
                ViewBag.DefaultShiftNumber = 1;
                model.ProductInId = 0;
                model.ProductOutId = 0;
            }
            else
            {
                model.ProductionDate = lastProduction.ProductionDate.AddDays(1);
                model.ShiftDetails = lastProduction.ShiftDetails;
                model.ShiftId = lastProduction.ShiftId;

                // Carry forward machine
                model.MachineId = lastProduction.MachineId;
                model.DepartmentId = lastProduction.DepartmentId;

                // Try to find previous products for same machine
                var prev = await _context.Productions
                    .Where(p => p.MachineId == lastProduction.MachineId)
                    .OrderByDescending(p => p.ProductionDate)
                    .FirstOrDefaultAsync();

                model.ProductInId = prev?.ProductInId ?? 0;
                model.ProductOutId = prev?.ProductOutId ?? 0;

                ViewBag.DefaultShiftNumber = GetShiftNumberFromDetails(lastProduction.ShiftDetails);
            }

            // Load all products for Product In
            var allProducts = await _context.Products.ToListAsync();
            ViewData["Products"] = new SelectList(allProducts, "Id", "Name");

            // Load Product Out filtered by department
            var departmentId = model.DepartmentId;
            var filteredProductOuts = _context.Products.AsQueryable();
            if (departmentId != 0)
            {
                filteredProductOuts = filteredProductOuts
                    .Where(p => p.ProductType.DepartmentId == departmentId);
            }
            ViewData["FilteredProductOuts"] = new SelectList(await filteredProductOuts.ToListAsync(), "Id", "Name");


            if (lastProduction == null)
            {
                model.OpeningHank = null;
            }
            else
            {
                var lastSameMachine = await _context.Productions
                    .Where(p => p.MachineId == lastProduction.MachineId)
                    .OrderByDescending(p => p.ProductionDate)
                    .ThenByDescending(p => p.Id)
                    .FirstOrDefaultAsync();

                model.OpeningHank = lastSameMachine?.ClosingHank;
            }


            ViewBag.PreviousShiftDetails = model.ShiftDetails;
            ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name", model.DepartmentId);
            ViewData["Machines"] = new SelectList(_context.Machines, "Id", "Name", model.MachineId);

            return View(model);
        }



        private int GetShiftNumberFromDetails(string? shiftDetails)
        {
            if (string.IsNullOrWhiteSpace(shiftDetails)) return 1;

            var match = System.Text.RegularExpressions.Regex.Match(shiftDetails, @"Shift\s*(\d+)");
            return match.Success ? int.Parse(match.Groups[1].Value) : 1;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Production production)
        {
            if (production.ShiftId == 0)
            {
                var fallbackShift = await _context.Shifts
                    .Where(s => s.EffectiveDate <= production.ProductionDate)
                    .OrderByDescending(s => s.EffectiveDate)
                    .FirstOrDefaultAsync();

                if (fallbackShift != null)
                {
                    production.ShiftId = fallbackShift.Id;
                }
            }

            // ✅ Calculate DelHank
            if (production.OpeningHank.HasValue && production.ClosingHank.HasValue)
            {
                int open = (int)production.OpeningHank.Value;
                int close = (int)production.ClosingHank.Value;
                int max = Math.Max(open, close);
                int digits = max.ToString().Length;
                int maxHank = (int)Math.Pow(10, digits) - 1;

                production.DelHank = close >= open
                    ? close - open
                    : (maxHank + 1 - open) + close;
            }

            if (ModelState.IsValid)
            {
                var prodDate = production.ProductionDate.ToDateTime(TimeOnly.MinValue);

        // Determine financial year
        var fyStart = new DateTime(prodDate.Year, 4, 1);
        if (prodDate.Month < 4)
            fyStart = fyStart.AddYears(-1);
        var fyEnd = fyStart.AddYears(1).AddDays(-1);

                // Get max ProductionNumber in current FY
                var nextNumber = _context.Productions
            .AsEnumerable() // ⬅️ Forces EF to load data and filter in memory
            .Where(p =>
                p.ProductionDate.ToDateTime(TimeOnly.MinValue) >= fyStart &&
                p.ProductionDate.ToDateTime(TimeOnly.MinValue) <= fyEnd)
            .Select(p => (int?)p.ProductionNumber)
            .Max() ?? 0;


                production.ProductionNumber = nextNumber + 1;

                _context.Add(production);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name", production.DepartmentId);
            ViewData["Machines"] = new SelectList(_context.Machines, "Id", "Name", production.MachineId);
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");

            return View(production);
        }



        [HttpGet]
        public async Task<IActionResult> GetShiftsByDate(DateOnly productionDate)
        {
            var shift = await _context.Shifts
                .Where(s => s.EffectiveDate <= productionDate)
                .OrderByDescending(s => s.EffectiveDate)
                .FirstOrDefaultAsync();

            if (shift == null || shift.TotalShifts == 0)
                return Json(new List<object>());

            var shiftOptions = new List<object>();

            for (int i = 1; i <= shift.TotalShifts; i++)
            {
                var (start, end) = i switch
                {
                    1 => (shift.StartTime1, shift.EndTime1),
                    2 => (shift.StartTime2, shift.EndTime2),
                    3 => (shift.StartTime3, shift.EndTime3),
                    4 => (shift.StartTime4, shift.EndTime4),
                    _ => (null, null)
                };

                if (start.HasValue && end.HasValue)
                {
                    shiftOptions.Add(new
                    {
                        id = $"{shift.Id}_{i}",
                        name = $"Shift {i}",
                        fromTime = start.Value.ToString("hh:mm tt"),
                        toTime = end.Value.ToString("hh:mm tt")
                    });
                }
            }

            return Json(shiftOptions);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var production = await _context.Productions
                .Include(p => p.Shift)
                .Include(p => p.Department)
                .Include(p => p.Machine)
                .Include(p => p.ProductIn)
                .Include(p => p.ProductOut)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (production == null)
                return NotFound();

            ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name", production.DepartmentId);
            ViewData["Machines"] = new SelectList(_context.Machines, "Id", "Name", production.MachineId);
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");

            return View(production);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductionDate,ShiftId,DepartmentId,MachineId,ShiftDetails,ShiftTime,RunTime,IdleTime,DelHank,TotalProduction,ProductionEfficiency,Bale,Lap,Mixing,NoOfDoffs,ConeWeight,OpeningKgs,Closing,SliverBreaks,ProductInId,ProductOutId,ExpectedProduction,ProductionDrop")] Production production)
        {
            if (id != production.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name", production.DepartmentId);
                ViewData["Machines"] = new SelectList(_context.Machines, "Id", "Name", production.MachineId);
                ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");
                return View(production);
            }

            try
            {
                _context.Update(production);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productions.Any(e => e.Id == production.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetLastClosingHank(int machineId)
        {
            var last = await _context.Productions
                .Where(p => p.MachineId == machineId)
                .OrderByDescending(p => p.ProductionDate)
                .ThenByDescending(p => p.Id)
                .FirstOrDefaultAsync();

            return Json(last?.ClosingHank ?? 0.00m);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var production = await _context.Productions
                .Include(p => p.Shift)
                .Include(p => p.Department)
                .Include(p => p.Machine)
                .Include(p => p.ProductIn)
                .Include(p => p.ProductOut)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (production == null)
                return NotFound();

            return View(production);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var production = await _context.Productions
                .Include(p => p.Shift)
                .Include(p => p.Department)
                .Include(p => p.Machine)
                .Include(p => p.ProductIn)
                .Include(p => p.ProductOut)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (production == null)
                return NotFound();

            return View(production);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var production = await _context.Productions.FindAsync(id);
            if (production != null)
            {
                _context.Productions.Remove(production);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByDepartment(int departmentId)
        {
            var products = await _context.Products
                .Where(p => p.ProductType.DepartmentId == departmentId)
                .Select(p => new { p.Id, p.Name })
                .ToListAsync();

            return Json(products);
        }

    }
}
