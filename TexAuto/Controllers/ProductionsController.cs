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

        public async Task<IActionResult> Create()
        {
            ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["Machines"] = new SelectList(_context.Machines, "Id", "Name");
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");
            return View();
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

            if (ModelState.IsValid)
            {
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
                        id = $"{shift.Id}_{i}", // <-- ensure it's a string
                        name = $"Shift {i}",
                        fromTime = start.Value.ToString("hh:mm tt"),
                        toTime = end.Value.ToString("hh:mm tt")
                    });
                }
            }

            return Json(shiftOptions);
        }

    }
}