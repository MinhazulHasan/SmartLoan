using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartLoan.ViewModels
{
    public class ReceiveOfficePaymentViewModel
    {
        public double TotalAmount { get; set; }
        public string UsersId { get; set; }
        public IList<int> InstallmentIds { get; set; }
        public SelectList PaymentMethods { get; set; }
    }
}
