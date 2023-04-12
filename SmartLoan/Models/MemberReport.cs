namespace SmartLoan.Models
{
    public class MemberReport
    {
        public string? GroupName { get; set; }
        public List<ReportTableForMemberLoan>? MemberReportTables { get; set; }
    }
}
