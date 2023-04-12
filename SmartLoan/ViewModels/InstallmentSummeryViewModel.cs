using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class InstallmentSummeryViewModel
    {
        public IEnumerable<InstallmentMaster> InstallmentMasters { get; set; }
        public string GroupName { get; set; }
        public string CollectorName { get; set; }
        public Loan Loan { get; set; }
        public LoanMaster LoanMaster { get; set; }
    }
}
