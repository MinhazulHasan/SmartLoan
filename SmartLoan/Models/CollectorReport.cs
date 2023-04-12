namespace SmartLoan.Models
{
    public class CollectorReport
    {
        public int GroupId { get; set; }
        public int InstallmentMasterId { get; set; }
        public int InstallmentNumber { get; set; }
        public string? GroupName { get; set; }
        public int PaymentMethod { get; set; }
        public string? SubmittorName { get; set; }
        public DateTime CollectionDate { get; set; }
        public double ReceivedAmount { get; set; }
        public double SubmittedAmountToOffice { get; set; }
    }
}
