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
    public class LedgerMastersController : Controller
    {
        private readonly TexAutoContext _context;

        public LedgerMastersController(TexAutoContext context)
        {
            _context = context;
        }

        // GET: LedgerMasters
        public async Task<IActionResult> Index()
        {
            var texAutoContext = _context.LedgerMaster.Include(l => l.GroupMaster);
            return View(await texAutoContext.ToListAsync());
        }

        // GET: LedgerMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledgerMaster = await _context.LedgerMaster
                .Include(l => l.GroupMaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ledgerMaster == null)
            {
                return NotFound();
            }

            return View(ledgerMaster);
        }

        // GET: LedgerMasters/Create
        public IActionResult Create()
        {
            var allIndianStates = new List<string>
{
            // States
            "Andhra Pradesh", "Arunachal Pradesh", "Assam", "Bihar", "Chhattisgarh", "Goa", "Gujarat", "Haryana", "Himachal Pradesh",
            "Jharkhand", "Karnataka", "Kerala", "Madhya Pradesh", "Maharashtra", "Manipur", "Meghalaya", "Mizoram", "Nagaland",
            "Odisha", "Punjab", "Rajasthan", "Sikkim", "Tamil Nadu", "Telangana", "Tripura", "Uttar Pradesh", "Uttarakhand", "West Bengal",

            // Union Territories
            "Andaman and Nicobar Islands", "Chandigarh", "Dadra and Nagar Haveli and Daman and Diu", "Delhi", "Jammu and Kashmir",
            "Ladakh", "Lakshadweep", "Puducherry"
        };

            ViewBag.GroupMasterId = new SelectList(_context.GroupMaster.Where(g => g.IsActive), "Id", "GroupName");

            ViewBag.LedgerTypes = new SelectList(new List<string> { "Supplier", "Customer", "Agent", "Transport", "General" });
            ViewBag.States = new SelectList(allIndianStates);
            ViewBag.BalanceTypes = new SelectList(new List<string> { "Debit", "Credit" });

            return View();
        }

        // POST: LedgerMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LedgerName,GroupMasterId,LedgerType,IsActive,MobileNumber,Email,Address,State,PinCode,GstNumber,PanNumber,IsTdsApplicable,OpeningBalance,BalanceType,AccountCode,BankName,BankAccountNumber,IfscCode,Remarks")] LedgerMaster ledgerMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ledgerMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate all dropdowns if model is invalid
            var allIndianStates = new List<string>
    {
        "Andhra Pradesh", "Arunachal Pradesh", "Assam", "Bihar", "Chhattisgarh", "Goa", "Gujarat", "Haryana", "Himachal Pradesh",
        "Jharkhand", "Karnataka", "Kerala", "Madhya Pradesh", "Maharashtra", "Manipur", "Meghalaya", "Mizoram", "Nagaland",
        "Odisha", "Punjab", "Rajasthan", "Sikkim", "Tamil Nadu", "Telangana", "Tripura", "Uttar Pradesh", "Uttarakhand", "West Bengal",
        "Andaman and Nicobar Islands", "Chandigarh", "Dadra and Nagar Haveli and Daman and Diu", "Delhi", "Jammu and Kashmir",
        "Ladakh", "Lakshadweep", "Puducherry"
    };

            ViewBag.GroupMasterId = new SelectList(_context.GroupMaster.Where(g => g.IsActive), "Id", "GroupName", ledgerMaster.GroupMasterId);
            ViewBag.States = new SelectList(allIndianStates, ledgerMaster.State);
            ViewBag.LedgerTypes = new SelectList(new List<string> { "Supplier", "Customer", "Agent", "Transport", "General" }, ledgerMaster.LedgerType);
            ViewBag.BalanceTypes = new SelectList(new List<string> { "Debit", "Credit" }, ledgerMaster.BalanceType);

            return View(ledgerMaster);
        }



        // GET: LedgerMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledgerMaster = await _context.LedgerMaster.FindAsync(id);
            if (ledgerMaster == null)
            {
                return NotFound();
            }
            ViewData["GroupMasterId"] = new SelectList(_context.Set<GroupMaster>(), "Id", "GroupName", ledgerMaster.GroupMasterId);
            return View(ledgerMaster);
        }

        // POST: LedgerMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LedgerName,GroupMasterId,LedgerType,IsActive,MobileNumber,Email,Address,State,PinCode,GstNumber,PanNumber,IsTdsApplicable,OpeningBalance,BalanceType,AccountCode,BankName,BankAccountNumber,IfscCode,Remarks")] LedgerMaster ledgerMaster)
        {
            if (id != ledgerMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ledgerMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LedgerMasterExists(ledgerMaster.Id))
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
            ViewData["GroupMasterId"] = new SelectList(_context.Set<GroupMaster>(), "Id", "GroupName", ledgerMaster.GroupMasterId);
            return View(ledgerMaster);
        }

        // GET: LedgerMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledgerMaster = await _context.LedgerMaster
                .Include(l => l.GroupMaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ledgerMaster == null)
            {
                return NotFound();
            }

            return View(ledgerMaster);
        }

        // POST: LedgerMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ledgerMaster = await _context.LedgerMaster.FindAsync(id);
            if (ledgerMaster != null)
            {
                _context.LedgerMaster.Remove(ledgerMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LedgerMasterExists(int id)
        {
            return _context.LedgerMaster.Any(e => e.Id == id);
        }
    }
}
