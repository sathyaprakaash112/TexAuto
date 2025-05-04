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

        // GET: Productions
        public async Task<IActionResult> Index()
        {
            var texAutoContext = _context.Productions.Include(p => p.Shift);
            return View(await texAutoContext.ToListAsync());
        }

        // GET: Productions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions
                .Include(p => p.Shift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // GET: Productions/Create
        public IActionResult Create()
        {
            ViewData["ShiftList"] = new List<SelectListItem>();
            ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["Machines"] = new SelectList(_context.Machines, "Id", "Name");
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductionDate,ShiftId,DepartmentId,MachineId,ShiftDetails,ShiftTime,RunTime,IdleTime,DelHank,TotalProduction,ProductionEfficiency,Bale,Lap,Mixing,NoOfDoffs,ConeWeight,OpeningKgs,Closing,SliverBreaks,ProductInId,ProductOutId,ExpectedProduction,ProductionDrop")] Production production)
        {
            if (ModelState.IsValid)
            {
                _context.Add(production);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload dropdowns on failure
            ViewData["ShiftList"] = new List<SelectListItem>();
            ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name", production.DepartmentId);
            ViewData["Machines"] = new SelectList(_context.Machines, "Id", "Name", production.MachineId);
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");

            return View(production);
        }



        // GET: Productions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var production = await _context.Productions
                .Include(p => p.Shift)
                .Include(p => p.Department)
                .Include(p => p.Machine)
                .Include(p => p.ProductIn)
                .Include(p => p.ProductOut)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (production == null) return NotFound();

            // Load shift options based on production date
            var shifts = await _context.Shifts
                .Where(s => s.EffectiveDate <= production.ProductionDate)
                .OrderBy(s => s.EffectiveDate)
                .ToListAsync();

            // Dropdown lists
            ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name", production.DepartmentId);
            ViewData["Machines"] = new SelectList(_context.Machines, "Id", "Name", production.MachineId);
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");

            // Optional: Shift list for display if needed
            ViewData["ShiftList"] = shifts.Select((s, index) => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = (index + 1).ToString()
            }).ToList();

            return View(production);
        }


        // POST: Productions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductionDate,ShiftId,DepartmentId,MachineId,ShiftDetails,ShiftTime,RunTime,IdleTime,DelHank,TotalProduction,ProductionEfficiency,Bale,Lap,Mixing,NoOfDoffs,ConeWeight,OpeningKgs,Closing,SliverBreaks,ProductInId,ProductOutId,ExpectedProduction,ProductionDrop")] Production production)
        {
            if (id != production.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(production);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionExists(production.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "Id", "Id", production.ShiftId);
            return View(production);
        }

        // GET: Productions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions
                .Include(p => p.Shift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var production = await _context.Productions.FindAsync(id);
            if (production != null)
            {
                _context.Productions.Remove(production);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionExists(int id)
        {
            return _context.Productions.Any(e => e.Id == id);
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
                        fromTime = start.Value.ToString("HH:mm"),
                        toTime = end.Value.ToString("HH:mm")
                    });
                }
            }

            return Json(shiftOptions);
        }

    }
}
