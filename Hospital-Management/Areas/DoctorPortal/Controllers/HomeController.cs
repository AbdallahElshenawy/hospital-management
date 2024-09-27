using Hospital_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Areas.DoctorPortal.Controllers
{
    [Area("DoctorPortal")]
    [Authorize(Policy = "RequireDoctorRole")]
    public class HomeController(UserManager<ApplicationUser> userManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(User);
            ApplicationUser user = await userManager.FindByIdAsync(userId);

            var model = new
            {
                UserName = user.UserName 
            };

            return View(model);
        }
    }
}
