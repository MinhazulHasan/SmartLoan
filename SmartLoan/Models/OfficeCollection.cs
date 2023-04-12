using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class OfficeCollection
    {
        [Key]
        public int Id { get; set; }
        public int GroupInfoId { get; set; }
        public Collection Collection { get; set; }
        public int CollectionId { get; set; }
        public string CollectorId { get; set; }
        public string SubmitterId { get; set; }
        public double Amount { get; set; }
        public DateTime CollectionDate { get; set; }
        public int PaymentMethod { get; set; }
    }
}
