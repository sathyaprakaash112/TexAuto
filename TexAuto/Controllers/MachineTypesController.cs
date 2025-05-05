using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TexAuto.Data;
using TexAuto.Models.Domain.Creation;

namespace TexAuto.Controllers
{
    public class MachineTypesController : Controller
    {
        private readonly TexAutoContext _context;

        public MachineTypesController(TexAutoContext context)
        {
            _context = context;
        }

        // GET: MachineTypes
        public async Task<IActionResult> Index()
        {
            var machineTypes = _context.MachineTypes.Include(m => m.Department);
            return View(await machineTypes.ToListAsync());
        }

        // GET: MachineTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var machineType = await _context.MachineTypes
                .Include(m => m.Department)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (machineType == null) return NotFound();

            return View(machineType);
        }

        // GET: MachineTypes/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: MachineTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DepartmentId")] MachineType machineType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(machineType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", machineType.DepartmentId);
            return View(machineType);
        }

        // GET: MachineTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var machineType = await _context.MachineTypes.FindAsync(id);
            if (machineType == null) return NotFound();

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", machineType.DepartmentId);
            return View(machineType);
        }

        // POST: MachineTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DepartmentId")] MachineType machineType)
        {
            if (id != machineType.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machineType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineTypeExists(machineType.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", machineType.DepartmentId);
            return View(machineType);
        }

        // GET: MachineTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var machineType = await _context.MachineTypes
                .Include(m => m.Department)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (machineType == null) return NotFound();

            return View(machineType);
        }

        // POST: MachineTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var machineType = await _context.MachineTypes.FindAsync(id);
            if (machineType != null)
            {
                _context.MachineTypes.Remove(machineType);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MachineTypeExists(int id)
        {
            return _context.MachineTypes.Any(e => e.Id == id);
        }
    }
}
