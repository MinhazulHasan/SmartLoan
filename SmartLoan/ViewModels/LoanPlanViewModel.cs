using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class LoanPlanViewModel
    {
        public IEnumerable<MemberInfo> MemberInfos { get; set; }
        public GroupInfo GroupInfo { get; set; }
        public Loan Loan { get; set; }
        public LoanMaster LoanMaster { get; set; }
        public IList<LoanDetail> LoanDetails { get; set; }

    }
}
