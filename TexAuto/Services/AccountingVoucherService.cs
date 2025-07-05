using Project.Models.Domain.Accounting;
using Project.Models.Domain.Creation;
using TexAuto.Data;
using Microsoft.EntityFrameworkCore;

namespace Project.Services
{
    public class AccountingVoucherHeaderService
    {
        private readonly TexAutoContext _context;

        public AccountingVoucherHeaderService(TexAutoContext context)
        {
            _context = context;
        }

        public async Task<AccountingVoucherHeader> CreateVoucherFromBalePurchaseAsync(BalePurchase purchase)
        {
            var voucher = new AccountingVoucherHeader
            {
                VoucherNo = GenerateVoucherNo(purchase.InwardDate),
                VoucherType = "PURCHASE",
                VoucherDate = purchase.InwardDate,
                ReferenceNo = purchase.BillNo,
                Narration = $"Being cotton bale purchased from {purchase.Supplier} including freight and commission",
                PartyLedger = purchase.Supplier?.Name,
                LinkedModule = "BALE_PURCHASE",
                LinkedRecordId = purchase.InwardNo.ToString(),
                Status = "POSTED",
                CreatedBy = "system",
                CreatedTime = DateTime.Now,
                IsAuto = true,
                Entries = new List<AccountingVoucherEntry>()
            };

            void AddDr(string ledger, decimal amount, string remarks, bool isTax = false)
            {
                if (amount > 0)
                {
                    voucher.Entries.Add(new AccountingVoucherEntry
                    {
                        Ledger = ledger,
                        DrCr = "Dr",
                        Amount = amount,
                        Remarks = remarks,
                        IsTaxLine = isTax
                    });
                }
            }

            void AddCr(string ledger, decimal amount, string remarks, bool isTax = false)
            {
                if (amount > 0)
                {
                    voucher.Entries.Add(new AccountingVoucherEntry
                    {
                        Ledger = ledger,
                        DrCr = "Cr",
                        Amount = amount,
                        Remarks = remarks,
                        IsTaxLine = isTax
                    });
                }
            }

            // Debit Entries
            AddDr("Bale Purchase A/C", purchase.TaxableAmount, "Cotton bale base purchase");
            AddDr("Freight Inward A/C", purchase.FreightCharges, "Freight from supplier");
            AddDr("Commission Paid A/C", purchase.CommissionAmount, "Agent commission");
            AddDr("Input CGST A/C", purchase.CgstAmount, "CGST", isTax: true);
            AddDr("Input SGST A/C", purchase.SgstAmount, "SGST", isTax: true);
            AddDr("Input IGST A/C", purchase.IgstAmount, "IGST", isTax: true);

            // Credit Entries
            AddCr("TDS Payable A/C", purchase.TdsAmount, "TDS deduction", isTax: true);
            AddCr(purchase.Supplier?.Name, purchase.PayableAmount, "Total payable to party");


            // Totals
            voucher.TotalDebit = voucher.Entries.Where(e => e.DrCr == "Dr").Sum(e => e.Amount);
            voucher.TotalCredit = voucher.Entries.Where(e => e.DrCr == "Cr").Sum(e => e.Amount);

            _context.AccountingVoucherHeaders.Add(voucher);
            await _context.SaveChangesAsync();

            return voucher;
        }

        private string GenerateVoucherNo(DateTime voucherDate)
        {
            // Determine financial year base
            int year = voucherDate.Month >= 4 ? voucherDate.Year : voucherDate.Year - 1;

            // Filter vouchers by financial year
            var fyVouchers = _context.AccountingVoucherHeaders
                .Where(v => (v.VoucherDate.Month >= 4 ? v.VoucherDate.Year : v.VoucherDate.Year - 1) == year);

            int next = (fyVouchers.Count() + 1);

            return $"VCH-{10000 + next}";
        }

    }
}
