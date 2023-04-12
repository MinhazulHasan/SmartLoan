using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class ChangePassword
    {
        [Required]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword",ErrorMessage ="New Password and confirm password are not same")]
        public string ConfirmPassword { get; set; }
    }
}
