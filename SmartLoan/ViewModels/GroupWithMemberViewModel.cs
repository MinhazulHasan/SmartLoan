using Microsoft.AspNetCore.Mvc.Rendering;
using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class GroupWithMemberViewModel
    {
        public IEnumerable<MemberInfo> MemberInfos { get; set; }
        public SelectList Groups { get; set; }
    }
}
