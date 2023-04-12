using Microsoft.AspNetCore.Mvc.Rendering;
using SmartLoan.Dtos;
using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class CollectionViewModel
    {
        public List<SelectListItem> ListItem { get; set; }
        public CollectionDto CollectionDto { get; set; }
        public IEnumerable<Collection> Collections { get; set; }
        public List<SelectListItem> CollectorList { get; set; }
        public Collection SingleCollection { get; set; }
    }
}
