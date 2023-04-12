using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class MemberReportViewModel
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Nid { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public string MobileNumber { get; set; }
        public string GroupName { get; set; }
        public List<ReportTableForMemberLoan> ReportTableForMemberLoans { get; set; }
        public List<ReportTableForMemberInstallment> ReportTableForMemberInstallments { get; set; }
    }
}
