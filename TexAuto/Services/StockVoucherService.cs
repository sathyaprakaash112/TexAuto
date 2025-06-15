using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Models.Domain.Vouchers;
using TexAuto.Data;
using TexAuto.Models.Domain.Entries;

namespace Project.Services
{
    public class StockVoucherService
    {
        private readonly TexAutoContext _context;

        public StockVoucherService(TexAutoContext context)
        {
            _context = context;
        }

        public async Task<StockVoucher> CreateFromBalePurchaseAsync(string referenceNo, string narration, string linkedRecordId, List<StockVoucherLineItem> items)
        {
            decimal totalQty = items.Sum(i => i.Quantity);
            decimal totalValue = items.Sum(i => i.Amount);

            var voucher = new StockVoucher
            {
                StockVoucherNo = GenerateStockVoucherNo(),
                StockVoucherType = "INWARD",
                StockVoucherDate = DateTime.Now,
                ReferenceNo = referenceNo,
                Narration = narration,
                LinkedModule = "BALE_PURCHASE",
                LinkedRecordId = linkedRecordId,
                TotalQuantity = totalQty,
                TotalValue = totalValue,
                Location = items.FirstOrDefault()?.Department,
                Status = "POSTED",
                CreatedBy = "admin", // You may replace this with actual user
                CreatedTime = DateTime.Now,
                IsAuto = true,
                LineItems = items
            };

            _context.StockVouchers.Add(voucher);
            await _context.SaveChangesAsync();

            return voucher;
        }

        public async Task<StockVoucher> CreateFromProductionAsync(Production production)
        {
            var lineItem = new StockVoucherLineItem
            {
                LineNo = 1,
                LotNo = $"PROD-{production.Id:D5}",
                ItemName = production.ProductOut?.Name ?? "Unknown",
                Quantity = production.TotalProduction,
                NumberOfBales = (int)production.Bale,
                RatePerKg = 0,
                Amount = 0,
                Department = production.Department?.Name,
                Remarks = "Production Output Entry"
            };
            var voucher = new StockVoucher
            {
                StockVoucherNo = GenerateStockVoucherNo(), // You can auto-generate
                StockVoucherType = "INWARD",
                StockVoucherDate = production.ProductionDate.ToDateTime(TimeOnly.MinValue),
                ReferenceNo = production.Id.ToString(),
                Narration = $"Production stock entry for {production.Department}",
                LinkedModule = "PRODUCTION",
                LinkedRecordId = production.Id.ToString(),
                Location = production.Department?.Name,
                Status = "POSTED",
                CreatedBy = "system",
                CreatedTime = DateTime.Now,
                IsAuto = true,
                LineItems = new List<StockVoucherLineItem> { lineItem }
            };


            // Calculate totals
            voucher.TotalQuantity = voucher.LineItems.Sum(x => x.Quantity);
            voucher.TotalValue = voucher.LineItems.Sum(x => x.Amount);

            _context.StockVouchers.Add(voucher);
            await _context.SaveChangesAsync();
            return voucher;
        }


        private string GenerateStockVoucherNo()
        {
            var last = _context.StockVouchers
                .OrderByDescending(v => v.Id)
                .FirstOrDefault();

            int nextNumber = 2030;
            if (last != null && last.StockVoucherNo.StartsWith("STK-"))
            {
                var number = last.StockVoucherNo.Substring(4);
                if (int.TryParse(number, out int lastNum))
                    nextNumber = lastNum + 1;
            }

            return $"STK-{nextNumber}";
        }
    }
}
