using Hospital_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class DoctorController(IDoctorServices doctorServices) : Controller
    {
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var doctors = await doctorServices.GetDoctors(page, pageSize);

            return View(doctors);
        }

        public async Task<IActionResult> Search(string searchString, int page = 1, int pageSize = 10, string? searchProperty = null)
        {
            var doctors = await doctorServices.Search(page, pageSize, searchString, searchProperty);

            ViewBag.SearchString = searchString;
            ViewBag.SearchProperty = searchProperty;

            return View("Index", doctors);
        }
    }
}
