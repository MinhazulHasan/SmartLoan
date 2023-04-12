using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class LoanDetail
    {
        [Key]
        public int Id { get; set; }
        public LoanMaster LoanMaster { get; set; }
        public int LoanMasterId { get; set; }
        public int GroupInfoId { get; set; }
        public MemberInfo MemberInfo { get; set; }
        public int MemberInfoId { get; set; }
        public double Amount { get; set; }
        public int Period { get; set; }
        public int Installment { get; set; }
        public double InterestRate { get; set; }
        public double ProcessingFee { get; set; }
        public double OtherCharge { get; set; }
        public double TotalAmount { get; set; }
        public double TotalFee { get; set; }
        public double AmountPerInstallment { get; set; }
        public bool InProcess { get; set; } = true;
    }
}
