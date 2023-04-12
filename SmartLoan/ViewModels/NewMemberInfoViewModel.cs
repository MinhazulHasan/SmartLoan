using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class NewMemberInfoViewModel
    {
        public MemberInfo MemberInfo { get; set; }
        public IEnumerable<MarritialStatus> MarritialStatus { get; set; }
        public int GroupId { get; set; }
    }
}
