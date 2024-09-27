using Hospital_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class SpecialityController(ISpecialityServices specialityServices) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var specialities = await specialityServices.GetSpecialities();

            return View(specialities);
        }

        [HttpGet("Speciality/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var speciality = await specialityServices.GetById(id);

            return View(speciality);
        }
    }
}
