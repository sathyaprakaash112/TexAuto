using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models.Domain.Creation;
using System.Linq;
using TexAuto.Data;
using X.PagedList;
using X.PagedList.Extensions;

namespace Project.Controllers
{
    public class BalePurchasesController : Controller
    {
        private readonly TexAutoContext _context;

        public BalePurchasesController(TexAutoContext context)
        {
            _context = context;
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
        public IActionResult Create(BalePurchase purchase)
        {
            if (ModelState.IsValid)
            {
                // Auto increment InwardNo safely
                int maxInward = _context.BalePurchases.Max(x => (int?)x.InwardNo) ?? 0;
                purchase.InwardNo = maxInward + 1;
                purchase.LotNo = maxInward + 1;

                _context.BalePurchases.Add(purchase);
                _context.SaveChanges();
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


        
    }
}
