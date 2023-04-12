using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class GroupInfo
    {
        public int Id { get; set; }


        [Required]
        [StringLength(100)]
        [Display(Name = "Group Name")]
        public string Name { get; set; }


        [StringLength(100)]
        [Display(Name = "Group Created By")]
        public string? GroupCreatorName { get; set; }


        [Required]
        [StringLength(500)]
        [Display(Name = "Group Location")]
        public string Location { get; set; }


        [Required]
        [Display(Name = "Time to create the group")]
        public DateTime GroupStart { get; set; } = DateTime.Now;


        public MemberInfo? MemberInfo { get; set; }


        [Display(Name = "Group Leader")]
        public int? GroupLeaderId { get; set; }


        [Range(10, 20000, ErrorMessage = "Enter a value between 10 to 20,000")]
        [Display(Name = "Amount to deposit")]
        public int AmountToDeposit { get; set; }


        public SubmissionPeriod SubmissionPeriod { get; set; }


        [Display(Name = "Submission period")]
        public int? SubmissionPeriodId { get; set; }
    }
}
