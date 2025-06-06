using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models.Domain.Masters;
using TexAuto.Data;

namespace TexAuto.Controllers
{
    public class GroupMastersController : Controller
    {
        private readonly TexAutoContext _context;

        public GroupMastersController(TexAutoContext context)
        {
            _context = context;
        }

        // GET: GroupMasters
        public async Task<IActionResult> Index()
        {
            var texAutoContext = _context.GroupMaster.Include(g => g.UnderGroup);
            return View(await texAutoContext.ToListAsync());
        }

        // GET: GroupMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupMaster = await _context.GroupMaster
                .Include(g => g.UnderGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupMaster == null)
            {
                return NotFound();
            }

            return View(groupMaster);
        }

        // GET: GroupMasters/Create
        public IActionResult Create()
        {
            ViewData["UnderGroupId"] = new SelectList(_context.GroupMaster, "Id", "GroupName");
            return View();
        }

        // POST: GroupMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupName,UnderGroupId,NatureOfGroup,AffectsInventory,IsDefault,IsActive,Remarks")] GroupMaster groupMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnderGroupId"] = new SelectList(_context.GroupMaster, "Id", "GroupName", groupMaster.UnderGroupId);
            return View(groupMaster);
        }

        // GET: GroupMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupMaster = await _context.GroupMaster.FindAsync(id);
            if (groupMaster == null)
            {
                return NotFound();
            }
            ViewData["UnderGroupId"] = new SelectList(_context.GroupMaster, "Id", "GroupName", groupMaster.UnderGroupId);
            return View(groupMaster);
        }

        // POST: GroupMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupName,UnderGroupId,NatureOfGroup,AffectsInventory,IsDefault,IsActive,Remarks")] GroupMaster groupMaster)
        {
            if (id != groupMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupMasterExists(groupMaster.Id))
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
            ViewData["UnderGroupId"] = new SelectList(_context.GroupMaster, "Id", "GroupName", groupMaster.UnderGroupId);
            return View(groupMaster);
        }

        // GET: GroupMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupMaster = await _context.GroupMaster
                .Include(g => g.UnderGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupMaster == null)
            {
                return NotFound();
            }

            return View(groupMaster);
        }

        // POST: GroupMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupMaster = await _context.GroupMaster.FindAsync(id);
            if (groupMaster != null)
            {
                _context.GroupMaster.Remove(groupMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupMasterExists(int id)
        {
            return _context.GroupMaster.Any(e => e.Id == id);
        }
    }
}
