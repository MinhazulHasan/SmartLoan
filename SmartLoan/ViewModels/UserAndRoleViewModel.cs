using Microsoft.AspNetCore.Identity;
using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class UserAndRoleViewModel
    {
        public List<IdentityUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<UsersLoginSession> UserLoginSessions { get; set; }
        public bool IsAdminUser { get; set; }
    }
}
