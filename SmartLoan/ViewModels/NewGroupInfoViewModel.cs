using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class NewGroupInfoViewModel
    {
        public GroupInfo GroupInfo { get; set; }
        public IEnumerable<MemberInfo> MemberInfo { get; set; }
        public IEnumerable<SubmissionPeriod> SubmissionPeriod { get; set; }
    }
}
