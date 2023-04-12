using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class LoanMaster
    {
        [Key]
        public int Id { get; set; }
        public Loan Loan { get; set; }
        public int LoanId { get; set; }
        public int GroupInfoId { get; set; }
        public double ExpectedTotalAmount { get; set; } = 0;
        public double CollectedTotalAmount { get; set; } = 0;
        public double PaymentInCash { get; set; } = 0;
        public double OnlinePayment { get; set; } = 0;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public int InstallmentCount { get; set; } = 0;
        public bool InProgress { get; set; } = true;
        public int InstallmentComplete { get; set; } = 0;
    }
}
