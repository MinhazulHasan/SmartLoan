using Microsoft.AspNetCore.Mvc.Rendering;
using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class LoanOfficeSubmissionFormViewModel
    {
        public IEnumerable<InstallmentDetails> InstallmentDetails { get; set; }
        public double TotalAmount { get; set; }
        public SelectList Users { get; set; }
        public string UserId { get; set; }
        public IList<int> InstallmentIds { get; set; }
        public SelectList PaymentMethods { get; set; }
        public int PaymentId { get; set; }
    }
}
