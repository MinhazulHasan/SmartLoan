using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class GroupLoanTakenViewModel
    {
        public List<GroupInfo> GroupInfos { get; set; }
        public List<int> LoanTakenList { get; set; }
    }
}
