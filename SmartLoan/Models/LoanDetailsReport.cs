namespace SmartLoan.Models
{
    public class LoanDetailsReport
    {
        public int MasterId { get; set; }
        public string? MemberName { get; set;}
        public string? GroupName { get; set;}
        public double Amount { get; set; }
        public int Period { get; set; }
        public int Installment { get; set; }
        public double InterestRate { get; set; }
        public double ProcessingFee { get; set; }
        public double OtherCharge { get; set; }
        public double TotalAmount { get; set; }
        public double TotalFee { get; set; }
    }
}
