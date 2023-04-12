using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLoan.Models;
using SmartLoan.ViewModels;

namespace SmartLoan.Controllers
{
    // [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,SignInManager<IdentityUser> signInManager,ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();
            var userLoginSession = _db.UsersLoginSessions.ToList();
            var user = await _userManager.GetUserAsync(User);
            bool IsAdmin = _userManager.IsInRoleAsync(user, "Admin").Result;
            var viewModel = new UserAndRoleViewModel()
            {
                Roles = roles,
                Users = users,
                UserLoginSessions = userLoginSession,
                IsAdminUser = IsAdmin
            };
            return View(viewModel);
        }
        public async Task<IActionResult> UserProfile()
        {
            List<IdentityRole> roles = _roleManager.Roles.ToList();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var viewModel = new UserProfileViewModel()
            {
                Roles = roles,
                User = user,
            };
            return View(viewModel);
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if(user == null)
                {
                    return RedirectToAction("Login", "RegistrationAndLogin");
                }
                var result = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
                if(result.Succeeded)
                {
                    TempData["PasswordChanged"] = "Password changed successfully!";
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("UserProfile");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }

            }

            return View(changePassword);
        }
        [AllowAnonymous]
        [Route("/User/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token,string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return BadRequest();
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData["Email-Confirmed"] = "Email is confirmed! Please login";
                return RedirectToAction("Login", "RegistrationAndLogin");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ToggleStatus(string id)
        {
            var session = _db.UsersLoginSessions.FirstOrDefault(u => u.UserId == id);
            if(session== null)
            {
                return BadRequest();
            }
            session.Active = !session.Active;
            _db.UsersLoginSessions.Update(session);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
