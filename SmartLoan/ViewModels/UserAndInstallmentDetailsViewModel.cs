using Microsoft.AspNetCore.Identity;
using SmartLoan.Models;

namespace SmartLoan.ViewModels
{
    public class UserAndInstallmentDetailsViewModel
    {
        public IdentityUser User;
        public List<InstallmentMaster> InstallmentMasters; 
    }
}
