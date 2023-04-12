using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CreatorId { get; set; }
        [Required]
        public DateTime? CreationDate { get; set; }
        [Required]
        public DateTime? CollectionDate { get; set; }
        [Required]
        [Range(minimum:1,maximum:double.MaxValue,ErrorMessage = "Value can't be less than 1")]
        public double EstimateAmount { get; set; }
        [Required]
        [Range(0,maximum:double.MaxValue,ErrorMessage = "Value can't be less than 0")]
        public double CollectedAmount { get; set; }
        public GroupInfo GroupInfo { get; set; }
        public int GroupInfoId { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public double PaymentInCash { get; set; }
        public double PaymentOnline { get; set; }
        public string CollectionId { get; set; }
        public string CollectorId { get; set; }
        public double OfficeSubmittedAmount { get; set; } = 0;

    }
}
