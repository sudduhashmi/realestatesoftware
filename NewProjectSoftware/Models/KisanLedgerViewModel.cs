using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tanyvas.Models
{
    public class KisanLedgerViewModel
    {
        public string KisanId { get; set; }
        public string KhasraNo { get; set; }
        public string InvestorId { get; set; }
        public string Name { get; set; }
        public int LandArea { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string Address { get; set; }
        public string InvestorPerc { get; set; }
        public string KisanCode { get; set; }
        public string InvestorCode { get; set; }

        public List<KisanLedgerDetailViewModel> KisanLedgerDetails { get; set; }
    }

    public class KisanLedgerDetailViewModel
    {
        public string KisanId { get; set; }
        public string TransactionDate { get; set; }
        public string Particulars { get; set; }
        public string Narration { get; set; }
        public decimal TotalAmount { get; set; }
        public int LedgerId { get; set; }
        public string PaymentMode { get; set; }
        public decimal CrAmount { get; set; }
        public decimal DrAmount { get; set; }
    }

}
