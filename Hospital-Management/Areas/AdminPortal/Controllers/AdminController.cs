using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Areas.AdminPortal.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
