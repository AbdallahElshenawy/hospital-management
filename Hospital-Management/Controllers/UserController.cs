using Hospital_Management.Models;
using Hospital_Management.Services;
using Hospital_Management.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hospital_Management.Controllers
{
    public class UserController(IUserServices<ApplicationUser> userServices, IEmailServices emailServices) : Controller
    {
        [Authorize(policy: "ConfirmedAccount")]
        public async Task<IActionResult> Index()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if(User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            var model = new RegisterationViewModel();

            return View("Register", model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Patient
                {
                    UserName = model.GenerateUsername(),
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.DateOfBirth,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = false,
                };

                var result = await userServices.Register(user, model.Password);

                if (result)
                {
                    await Login(new LoginViewModel { UsernameOrEmail = user.UserName, Password = model.Password, RememberMe = true });
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Register", model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View("Login", model);
            }

            var result = await userServices.Login(model.UsernameOrEmail, model.Password);

            if (result)
            {
                var user = await userServices.GetUserByUsernameAsync(User.Identity.Name);
                var roles = await userServices.GetUserRolesAsync(user);


                await SetClaimsAsync(user, roles, model.RememberMe);
                

                return roles switch
                {
                    _ when roles.Contains("Admin") => RedirectToAction("Index", "Home", new { area = "adminportal" }),
                    _ when roles.Contains("Doctor") => RedirectToAction("Index", "Home", new { area = "doctorportal" }),
                    _ when roles.Contains("Assistant") => RedirectToAction("Index", "Home", new { area = "assistantportal" }),
                    _ => RedirectToAction("Index", "Home")
                };
            }
            else
            {
                ModelState.AddModelError("", "An Error Occured While logging you in.");
                return View("Login", model);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View("AccessDenied");

        [Authorize(policy: "LoggedInUser")]
        public IActionResult NotConfirmedAccount() => View();

        [Authorize(policy: "LoggedInUser")]
        public async Task<IActionResult> SendConfirmationMail()
        {
            var user = await userServices.GetUserByUsernameAsync(User.Identity.Name);
            var token = await userServices.GenerateEmailConfirmationTokenAsync(user);
            var urlHelper = Url;

            try
            {
                await emailServices.SendConfirmationEmailAsync(user, token, urlHelper);
                ViewBag.Success = "A confirmation email has been sent to your email address. Please confirm your email to continue.";
            }
            catch (Exception)
            {
                ViewBag.Error = "An error occured while sending the confirmation email. Please try again later.";
                return View("NotConfirmedAccount");
            }

            return View("NotConfirmedAccount");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await userServices.GetUserByIdAsync(userId);

            var result = await userServices.ConfirmEmailAsync(user, token);

            if (!result)
            {
                return RedirectToAction("NotConfirmedAccount");
            }

            if (User.Identity.IsAuthenticated)
            {
                await userServices.Logout();

                SetClaimsAsync(user, await userServices.GetUserRolesAsync(user));
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid email address.");
                return View(model);
            }

            var user = await userServices.GetUserByEmailAsync(model.Email);

            if (user == null)
            {
                return View(model);
            }

            var token = await userServices.GenerateEmailPasswordResetTokenAsync(user);
            var urlHelper = Url;

            try
            {
                await emailServices.SendPasswordResetEmailAsync(user, token, urlHelper);
                ViewBag.Success = "A password reset email has been sent to your email address. Please reset your password to continue.";
            }
            catch (Exception)
            {
                ViewBag.Error = "An error occured while sending the password reset email. Please try again later.";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            var model = new ResetPasswordViewModel
            {
                UserId = userId,
                Token = token,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid password.");
                return View(model);
            }

            var user = await userServices.GetUserByIdAsync(model.UserId);

            if (user == null)
            {
                return View(model);
            }

            try
            {
                await userServices.ResetPasswordAsync(user, model.Token, model.NewPassword);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occured while resetting your password.");
                return View(model);
            }

            return RedirectToAction("Login");
        }


        public async Task SetClaimsAsync(ApplicationUser user, IList<string> roles, bool isPersistent = false)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString("yyyy-MM-dd")),
                    new Claim(ClaimTypes.AuthorizationDecision, (user.EmailConfirmed || user.PhoneNumberConfirmed).ToString()),
                };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                ExpiresUtc = isPersistent ? DateTimeOffset.UtcNow.AddDays(15) : null
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    }
}
