using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartLoan.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using MimeKit;
using MimeKit.Text;
using static System.Net.WebRequestMethods;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Web;

namespace SmartLoan.Controllers
{
    // [Authorize]
    public class RegistrationAndLoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;

        public RegistrationAndLoginController(SignInManager<IdentityUser> signManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db, IConfiguration config)
        {
            _signManager = signManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _config = config;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Registration()
        {
            var roles = _roleManager.Roles.ToList();
            var viewModel = new RegistrationCredential()
            {
                AllRoles = new SelectList(roles, "Id", "Name")
            };
            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationCredential credential)
        {
            if (credential == null)
            {
                return View(credential);
            }
            var user = new IdentityUser()
            {
                UserName = credential.Name,
                Email = credential.Email,
            };
            var result = await _userManager.CreateAsync(user, credential.ConfirmPassword);
            if (result.Succeeded)
            {
                var role = await _roleManager.FindByIdAsync(credential.Role);
                await _userManager.AddToRoleAsync(user,role.Name);
                var loginSession = new UsersLoginSession()
                {
                    UserId = user.Id,
                    Active = true,
                };
                _db.UsersLoginSessions.Add(loginSession);
                _db.SaveChanges();

                //Send email section start
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = HttpUtility.UrlEncode(token);
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Admin", _config.GetSection("EmailUserName").Value));
                email.To.Add(new MailboxAddress(credential.Name,credential.Email));
                email.Subject = "New Account on SmartLoan";
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = $"<h1>Hello {user.UserName}</h1> <p>An Admin of the SmartLoan created an account for you</p> <p> Please confirm your email before login by click on the given link </p> <a href='https://localhost:44306/User/ConfirmEmail?token={token}&email={credential.Email}'>Click Here </a> <p> Your email: <strong>{credential.Email}</strong></p> <p>Password: <strong> {credential.Password} </strong></p> <h5>Please change the password after login</h5>"
                };

                using (var smtp = new SmtpClient()) {
                    smtp.Connect(_config.GetSection("EmailHostName").Value, 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }


                //Send email section end

                return RedirectToAction("Index", "User");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return View(credential);
            }
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginCredential loginCredential)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginCredential.Email);
                if (user == null)
                {
                    TempData["Login-failed"] = "Email or password is not correct! or email is not confirmed yet";
                    return View(loginCredential);
                }

                var session = _db.UsersLoginSessions.FirstOrDefault(u => u.UserId == user.Id);
                if (session.Active == false)
                {
                    return View("Inactive");
                }
                var result = await _signManager.PasswordSignInAsync(user, loginCredential.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            TempData["Login-failed"] = "Email or password is not correct! or email is not confirmed yet";
            return View(loginCredential);
        }
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(IdentityRole identityRole)
        {
            if (!ModelState.IsValid)
            {
                return View(identityRole);
            }
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Registration");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                var roles = _roleManager.Roles.ToList();
                var viewModel = new RegistrationCredential()
                {
                    AllRoles = new SelectList(roles,"Id","Name")
                };
                return View("Registration",viewModel);
            }

        }
    }
}
