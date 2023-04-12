namespace SmartLoan.Models
{
    public class LoanMasterReport
    {
        public int LoanId { get; set; }
        public string? LoanName { get; set; }
        public double ExpectedTotalAmount { get; set; }
        public double CollectedTotalAmount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int InstallmentCount { get; set; }
        public int InstallmentComplete { get; set; }
        public bool InProgress { get; set; }
        public List<LoanDetailsReport>? LoanDetailsReports { get; set; }
    }
}
