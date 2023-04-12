using Microsoft.AspNetCore.Identity;
using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class CollectionHistoryViewModel
    {
        public List<CollectionReport> CollectionReports { get; set; }
        public MemberInfo MemberInfo { get; set; }
        public string GroupName { get; set; }
    }
}
