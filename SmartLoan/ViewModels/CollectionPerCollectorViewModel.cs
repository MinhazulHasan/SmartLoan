using Microsoft.AspNetCore.Identity;
using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class CollectionPerCollectorViewModel
    {
        public IEnumerable<OfficeCollection> OfficeCollectionsPerCollector { get; set; }
        public IdentityUser User { get; set; }
        public double CollectedAmount { get; set; }
        public double SubmittedAmount { get; set; }
    }
}
