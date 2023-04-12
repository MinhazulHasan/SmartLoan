using Microsoft.AspNetCore.Mvc.Rendering;
using SmartLoan.Dtos;
using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class OfficeCollectionViewModel
    {
        public List<SelectListItem> CollectorList { get; set; }
        public Collection SingleCollection { get; set; }
        public List<SelectListItem> PaymentMethod { get; set; }
        public OfficeCollectionDto OfficeCollectionDto { get; set; }
    }
}
