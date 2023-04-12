using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class DeleteMemberViewModel
    {
        public IEnumerable<MemberInfo> MemberInfos { get; set; }
        public int GroupId { get; set; }
    }
}
