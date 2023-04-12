using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class InstallmentMaster
    {
        [Key]
        public int Id { get; set; }
        public int InstallmentNumber { get; set; }
        public int GroupInfoId { get; set; }
        public LoanMaster LoanMaster { get; set; }
        public int LoanMasterId { get; set; }
        public double ExpectedAmount { get; set; }
        public double CollectedAmount { get; set; } = 0;
        public Status Status { get; set; }
        public int StatusId { get; set; } = 1;
        public DateTime CollectionDate { get; set; } = DateTime.Now;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public string CollectorId { get; set; }
        public double SubmittedAmountToOffice { get; set; } = 0;
    }
}
