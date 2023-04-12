namespace SmartLoan.Models
{
    public class ReportTableForMemberLoan
    {
        public int LoanMasterId { get; set; }
        public string LoanName { get; set; }
        public double Amount { get; set; }
        public int Period { get; set; }
        public int InstallmentNumber { get; set; }
        public double InterestRate { get; set; }
        public double ProcessingFee { get; set; }
        public double OtherCharge { get; set; }
        public double TotalAmount { get; set; }
        public double AmountPerInstallment { get; set; }
        public bool InProcess { get; set; }
    }
}
