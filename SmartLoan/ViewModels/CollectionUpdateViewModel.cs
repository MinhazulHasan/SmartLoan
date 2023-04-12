using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class CollectionUpdateViewModel
    {
        public Collection Collection { get; set; }
        public CollectionUpdate CollectionUpdate { get; set; }
        public IEnumerable<CollectionDetails> CollectionDetails { get; set; }
        public double TotalAmount { get; set; }
        public List<SelectListItem> PaymentMethodListItem { get; set; }
        public IdentityUser? CollectorInfo { get; set; }
        public string? NoCollector { get; set; }
    }
}
