using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class RegistrationCredential
    {
        [Display(Name = "User name")]
        [Required(ErrorMessage = "Name can't be empty!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email can't be empty or invalid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password can't be empty or less than 6 char")]
        [StringLength(maximumLength:64,MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Comfirm password can't be empty")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and comfirm password are not same")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name="Select a Role")]
        public string Role { get; set; }
        public SelectList AllRoles { get; set; }
        public IdentityRole IdentityRole { get; set; }
    }
}
