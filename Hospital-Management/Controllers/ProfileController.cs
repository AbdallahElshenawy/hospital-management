using Hospital_Management.Models;
using Hospital_Management.Models.DataTypes;
using Hospital_Management.Repository;
using Hospital_Management.Services;
using Hospital_Management.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hospital_Management.Controllers
{
    public class ProfileController(IUserServices<ApplicationUser> services,IUnitOfWork unit) : Controller
    {

       
        public async Task<IActionResult> Index()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await services.GetUserByIdAsync(userId);
            if (userId != null)
            {
                var model = new UserProfileDataViewModel
                {
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UserName = User.Identity.Name,
                    Email = User.FindFirst(ClaimTypes.Email)?.Value,
                    FirstName = User.FindFirst(ClaimTypes.GivenName)?.Value,
                    LastName = User.FindFirst(ClaimTypes.Surname)?.Value,
                    BirthDate = User.FindFirst(ClaimTypes.DateOfBirth)?.Value,
                    Img = user.Img
                };
                var roles = await services.GetUserRolesAsync(user);
                foreach (var role in roles)
                {
                    if (role == "patient")
                    {
                        Patient patient = user as Patient;
                        model.PatientReservation = patient.Reservations.Take(5).Where(r => r.ReservationStatus == ReservationStatus.Pending).ToList();
                    }
                    else if (role == "doctor")
                    {
                        Doctor doctor = user as Doctor;
                        model.DoctorRates = doctor.Rates.Take(5).Where(d => d.DoctorId == doctor.Id).ToList();

                    }


                }


                return View(model);
            }
            
              return  RedirectToAction("Login", "User");
            
        }
        [HttpPost]
        public async Task<IActionResult> SaveChanges(UserProfileDataViewModel model, IFormFile Img)
        { 
            
            if (ModelState.IsValid)
            {
                
                if (Img != null && Img.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Img.CopyToAsync(memoryStream);
                      
                        model.Img = memoryStream.ToArray();
                   
                    }
                  
                }

            }
            var user = await services.GetUserByIdAsync(model.UserId);
            if (user != null) 
            {
                user.Email =model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.BirthDate = DateTime.Parse(model.BirthDate);
                if (Img != null && Img.Length > 0)
                {
                    user.Img = model.Img;
                }
              
                var result = unit.Save();
                if (result!=0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    UpdateClaim(claimsIdentity, ClaimTypes.Email, model.Email);
                    UpdateClaim(claimsIdentity, ClaimTypes.GivenName, model.FirstName);
                    UpdateClaim(claimsIdentity, ClaimTypes.Surname, model.LastName);
                    if (DateTime.TryParse(model.BirthDate, out DateTime birthDate))
                    {
                        UpdateClaim(claimsIdentity, ClaimTypes.DateOfBirth, birthDate.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid date format for Birth Date.");
                    }
                    await HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity));


                    return RedirectToAction("index","profile"); 
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while saving your profile.");
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
            }

            return View("Index",model);

        }
        private void UpdateClaim(ClaimsIdentity identity, string claimType, string newValue)
        {
            
            var existingClaim = identity.FindFirst(claimType);
            if (existingClaim != null)
            {
      
                identity.RemoveClaim(existingClaim);
            }
            
                identity.AddClaim(new Claim(claimType, newValue));
            
           
           
        } 
        public async Task<IActionResult> VerifyUserName(string userName)
        {
            var testUser = await services.GetUserByUsernameAsync(userName);
            if (testUser != null)
            {
                return Json($"Email {userName} is already in use.");
            }
            return Json(true);
        }
        public async Task<IActionResult> VerifyUser(UserProfileDataViewModel model)
        {
            
            var existingUser = await services.GetUserByEmail(model.Email);
            if (existingUser != null && existingUser.Id != model.UserId)
            {
                return Json($"Username or Email is already in use.");
            }
            return Json(true);
        }
    }
}
