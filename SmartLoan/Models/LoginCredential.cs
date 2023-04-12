using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class LoginCredential
    {
        [Required(ErrorMessage = "Email can't be empty or invalid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password can't be empty or less than 6 char")]
        [StringLength(maximumLength: 64, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
