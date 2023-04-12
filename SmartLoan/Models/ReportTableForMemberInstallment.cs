namespace SmartLoan.Models
{
    public class ReportTableForMemberInstallment
    {
        public int InstallmentNumber { get; set; }
        public string CollectorName { get; set; }
        public int LoanMasterId { get; set; }
        public string LoanName { get; set; }
        public double InstallmentAmount { get; set; }
        public double ReceivedAmount { get; set; }
        public double Penalty { get; set; }
        public double DueAmount { get; set; }
        public int Status { get; set; }
        public DateTime InstallmentDate { get; set; }
        public DateTime ReceivedtDate { get; set; }
    }
}
