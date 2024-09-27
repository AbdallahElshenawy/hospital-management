using Hospital_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Areas.admin.Controllers
{
    [Area("AdminPortal")]
    [Authorize(Policy = "RequireAdminRole")]
    public class HomeController(UserManager<ApplicationUser> userManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(User);
            ApplicationUser user = await userManager.FindByIdAsync(userId);

            var model = new {userName= user.UserName };

            return View(model);
        }
    }
}
