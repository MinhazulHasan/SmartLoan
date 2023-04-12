using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class InstallmentDetails
    {
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int InstalllmentNumber { get; set; }
        public string CollectorId { get; set; }
        public LoanMaster LoanMaster { get; set; }
        public int LoanMasterId { get; set; }
        public int InstallmentMasterId { get; set; }
        public int LoanId { get; set; }
        public MemberInfo MemberInfo { get; set; }
        public int MemberInfoId { get; set; }
        public double InstallmentAmount { get; set; } = 0;
        public double ReceivedAmount { get; set; } = 0;
        public double PenaltyCharge { get; set; } = 0;
        public double DueAmount { get; set;} = 0;
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public DateTime InstallmentDate { get; set; } = DateTime.Now;
        public DateTime ReceivedDate { get; set;} = DateTime.Now;
        public bool SubmittedToOffice { get; set; } = false;
    }
}
