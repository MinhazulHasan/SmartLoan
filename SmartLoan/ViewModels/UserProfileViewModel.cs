using Microsoft.AspNetCore.Identity;

namespace SmartLoan.ViewModels
{
    public class UserProfileViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public IdentityUser User { get; set; }
    }
}
