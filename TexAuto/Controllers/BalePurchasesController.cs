using Microsoft.AspNetCore.Mvc;
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

        public BalePurchasesController(TexAutoContext context,AccountingVoucherHeaderService accountingVoucherHeaderService)
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
                .ToPagedList(pageNumber, pageSize); // Sync pagination

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
            // Get max Inward No from DB
            int maxInward = _context.BalePurchases.Max(x => (int?)x.InwardNo) ?? 0;

            // Create a default instance with pre-filled values
            var newPurchase = new BalePurchase
            {
                InwardNo = maxInward + 1,
                LotNo = (maxInward + 1), // If LotNo is text
                InwardDate = DateTime.Today,
                BillDate = DateTime.Today
            };

            return View(newPurchase);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BalePurchase purchase)
        {
            if (ModelState.IsValid)
            {
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

            return View(purchase);
        }


        public IActionResult Edit(int id)
        {
            var purchase = _context.BalePurchases
                .Include(p => p.BaleDetails)
                .FirstOrDefault(p => p.Id == id);

            if (purchase == null)
                return NotFound();

            return View(purchase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BalePurchase purchase)
        {
            if (id != purchase.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existing = _context.BalePurchases
                    .Include(p => p.BaleDetails)
                    .FirstOrDefault(p => p.Id == id);

                if (existing == null) return NotFound();

                // Update scalar fields
                _context.Entry(existing).CurrentValues.SetValues(purchase);

                // Remove existing children
                _context.BaleDetails.RemoveRange(existing.BaleDetails);

                // Ensure FK is set for new children
                foreach (var detail in purchase.BaleDetails)
                {
                    detail.BalePurchaseId = purchase.Id; // ✅ THIS LINE IS CRUCIAL
                }

                // Add new children
                _context.BaleDetails.AddRange(purchase.BaleDetails);

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(purchase);
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
