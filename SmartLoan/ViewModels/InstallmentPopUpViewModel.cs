using Microsoft.AspNetCore.Mvc.Rendering;
using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class InstallmentPopUpViewModel
    {
        public LoanMaster LoanMaster { get; set; }
        public string CollectorId { get; set; }
        public List<SelectListItem> CollectorList { get; set; }
    }
}
