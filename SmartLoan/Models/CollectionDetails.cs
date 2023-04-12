using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class CollectionDetails
    {
        [Key]
        public int Id { get; set; }
        public MemberInfo? MemberInfo { get; set; }
        public int MemberInfoId { get; set; }
        public int GroupId { get; set; }
        public int StatusId { get; set; }
        public string? CollectorId { get; set; }
        public DateTime? CollectionDate { get; set; }
        [Range(0,double.MaxValue)]
        public double LateFee { get; set; }
        public Collection? Collection { get; set; }
        public int CollectionId { get; set; }
        public double AmountToPay { get; set; }
        public byte PaymentMethodId { get; set; }
        public double PaymentInCash { get; set; }
        public double PaymentOnline { get; set; }
    }
}
