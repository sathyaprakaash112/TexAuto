using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models.Domain.Creation;
using Project.Services;
using System.Linq;
using TexAuto.Data;
using X.PagedList;
using X.PagedList.Extensions;

namespace Project.Controllers
{
    public class BalePurchasesController : Controller
    {
        private readonly TexAutoContext _context;
        private readonly AccountingVoucherHeaderService _voucherHeaderService;

        public BalePurchasesController(TexAutoContext context, AccountingVoucherHeaderService accountingVoucherHeaderService)
        {
            _context = context;
            _voucherHeaderService = accountingVoucherHeaderService;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var balePurchases = _context.BalePurchases
                .AsNoTracking()
                .OrderByDescending(b => b.InwardDate)
                .ToPagedList(pageNumber, pageSize);

            return View(balePurchases);
        }

        public IActionResult Details(int id)
        {
            var purchase = _context.BalePurchases.Find(id);
            if (purchase == null)
                return NotFound();

            return View(purchase);
        }

        public IActionResult Create()
        {
            int maxInward = _context.BalePurchases.Max(x => (int?)x.InwardNo) ?? 0;

            var newPurchase = new BalePurchase
            {
                InwardNo = maxInward + 1,
                LotNo = (maxInward + 1),
                InwardDate = DateTime.Today,
                BillDate = DateTime.Today
            };

            ViewBag.LedgerList = new SelectList(_context.LedgerMaster.Where(x => x.IsActive), "Id", "Name");
            ViewBag.ProductList = new SelectList(_context.Products.Where(x => x.ProductType.Name.Equals("Fibre")), "Name", "Name");

            return View(newPurchase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BalePurchase purchase)
        {
            if (ModelState.IsValid)
            {

                ViewBag.LedgerList = new SelectList(_context.LedgerMaster, "Id", "Name");
                ViewBag.ProductList = new SelectList(_context.Products.Where(x=>x.ProductType.Name.Equals("Fibre")), "Name", "Name");

                // Get financial year base from purchase date
                var inwardDate = purchase.InwardDate;
                int year = inwardDate.Month >= 4 ? inwardDate.Year : inwardDate.Year - 1;

                // Filter existing inward records for the same FY
                var fyInwards = _context.BalePurchases
                    .Where(p => (p.InwardDate.Month >= 4 ? p.InwardDate.Year : p.InwardDate.Year - 1) == year);

                int nextInwardNo = (fyInwards.Max(x => (int?)x.InwardNo) ?? 0) + 1;

                // Set inward and lot numbers
                purchase.InwardNo = nextInwardNo;
                purchase.LotNo = nextInwardNo;

                _context.BalePurchases.Add(purchase);
                await _context.SaveChangesAsync();

                await _voucherHeaderService.CreateVoucherFromBalePurchaseAsync(purchase);

                return RedirectToAction(nameof(Index));
            }

            // 🔁 Repopulate dropdowns on validation failure
            ViewBag.LedgerList = new SelectList(_context.LedgerMaster.Where(x => x.IsActive), "Id", "Name");

            return View(purchase);
        }



        public IActionResult Edit(int id)
        {
            var purchase = _context.BalePurchases
                .Include(p => p.BaleDetails)
                .FirstOrDefault(p => p.Id == id);

            if (purchase == null)
                return NotFound();

            ViewBag.LedgerList = new SelectList(_context.LedgerMaster.Where(x => x.IsActive), "Id", "Name");

            return View(purchase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BalePurchase purchase)
        {
            if (id != purchase.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.LedgerList = new SelectList(_context.LedgerMaster.Where(x => x.IsActive), "Id", "Name");
                return View(purchase);
            }

            var existing = _context.BalePurchases
                .Include(p => p.BaleDetails)
                .FirstOrDefault(p => p.Id == id);

            if (existing == null)
                return NotFound();

            _context.Entry(existing).CurrentValues.SetValues(purchase);
            _context.BaleDetails.RemoveRange(existing.BaleDetails);

            if (purchase.BaleDetails != null)
            {
                foreach (var detail in purchase.BaleDetails)
                {
                    detail.BalePurchaseId = purchase.Id;
                }

                _context.BaleDetails.AddRange(purchase.BaleDetails);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var purchase = _context.BalePurchases
                .Include(p => p.BaleDetails)
                .FirstOrDefault(p => p.Id == id);

            if (purchase == null)
                return NotFound();

            return View(purchase);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var purchase = _context.BalePurchases
                .Include(p => p.BaleDetails)
                .FirstOrDefault(p => p.Id == id);

            if (purchase != null)
            {
                _context.BaleDetails.RemoveRange(purchase.BaleDetails);
                _context.BalePurchases.Remove(purchase);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetNextInwardNo(DateTime inwardDate)
        {
            int year = inwardDate.Month >= 4 ? inwardDate.Year : inwardDate.Year - 1;

            var fyInwards = _context.BalePurchases
                .Where(p => (p.InwardDate.Month >= 4 ? p.InwardDate.Year : p.InwardDate.Year - 1) == year);

            int nextInwardNo = (fyInwards.Max(x => (int?)x.InwardNo) ?? 0) + 1;

            return Json(new { inwardNo = nextInwardNo });
        }
    }
}
