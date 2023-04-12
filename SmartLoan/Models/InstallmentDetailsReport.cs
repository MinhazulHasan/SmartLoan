namespace SmartLoan.Models
{
    public class InstallmentDetailsReport
    {
        public int? InstallmentNumber { get; set; }
        public int? MemberInfoId { get; set;}
        public string? MemberName { get; set;}
        public double? InstallmentAmount { get; set;}
        public double? ReceivedAmount { get; set;}
        public double? PenaltyCharge { get; set;}
        public double? DueAmount { get; set;}
        public int? Status { get; set;}
        public DateTime? InstallmentDate { get; set;}
        public DateTime? ReceivedtDate { get; set;}
    }
}
