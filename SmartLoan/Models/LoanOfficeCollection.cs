using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class LoanOfficeCollection
    {
        [Key]
        public int Id { get; set; }
        public InstallmentMaster InstallmentMaster { get; set; }
        public int InstallmentMasterId { get; set; }
        public int PaymentMethodId { get; set; }
        public string CollectorId { get; set; }
        public string SubmitterId { get; set; }
        public DateTime CollectionDate { get; set; } = DateTime.Now;
        public double AmountReceived { get; set; }

    }
}
