using Hospital_Management.Models;
using Hospital_Management.Repository;
using Hospital_Management.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hospital_Management.Controllers
{
    public class HomeController(IUserServices<ApplicationUser> userServices, IUnitOfWork unitOfWork, IHomeServices homeServices) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = await homeServices.GetHomeModel();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
