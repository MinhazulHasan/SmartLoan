using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class MemberInfoWithGroupViewModel
    {
        public int GroupId { get; set; }
        public IEnumerable<MemberInfo> MemberInfos { get; set; }
    }
}
