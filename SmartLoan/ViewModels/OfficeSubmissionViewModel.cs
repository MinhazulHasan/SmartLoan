using Microsoft.AspNetCore.Mvc.Rendering;
using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class OfficeSubmissionViewModel
    {
        public SelectList UsersList { get; set; }
        public LoanMaster LoanMaster { get; set; }
        public int LoanMasterId { get; set; }
        public GroupInfo GroupInfo { get; set; }
        public InstallmentMaster InstallmentMaster { get; set; }
        public int InstallmentMasterId { get; set; }
        public SelectList PaymentList { get; set; }
        public string CollectorId { get; set; }
        public string SubmitterId { get; set; }
        public string SubmitterName { get; set; }
        public double AmountSubmitted { get; set; }
        public byte PaymentMethodId { get; set; }
    }
}
