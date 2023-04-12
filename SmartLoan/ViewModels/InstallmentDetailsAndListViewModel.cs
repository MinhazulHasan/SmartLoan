using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class InstallmentDetailsAndListViewModel
    {
        public IEnumerable<InstallmentDetails> InstallmentDetails { get; set; }
        public IList<InstallmentDetails> InstallmentsList { get; set; }
        public InstallmentMaster InstallmentMaster { get; set; }
    }
}
