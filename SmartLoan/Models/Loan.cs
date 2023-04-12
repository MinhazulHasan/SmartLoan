using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        public GroupInfo GroupInfo { get; set; }
        public int GroupInfoId { get; set; }
        [StringLength(30,ErrorMessage = "Group Name length can't be greater than 30")]
        [Required(ErrorMessage = "Loan Name can't be empty")]
        [Display(Name = "Loan ID")]
        public string LoanId { get; set; }
    }
}
