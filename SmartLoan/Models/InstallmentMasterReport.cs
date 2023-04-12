namespace SmartLoan.Models
{
    public class InstallmentMasterReport
    {
        public int? GroupId { get; set; }
        public string? GroupName { get; set; }
        public int? InstallmentNum { get; set; }
        public double? ExpectedAmount { get; set; }
        public double? CollectedAmount { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string? CollectorName { get; set; }
        public List<InstallmentDetailsReport>? InstallmentDetailsReports { get; set; }
    }
}
