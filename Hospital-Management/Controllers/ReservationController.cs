using Hospital_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class ReservationController(IReservationServices reservationServices, IEmailServices emailServices) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> New(string doctorId)
        {
            var doctorWithRes = await reservationServices.GetDoctorWithResPropertiesAsync(doctorId);

            return View(doctorWithRes);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableAppointments(DateTime date, string doctorId)
        {
            var reservations = await reservationServices.GetReservationsInListAsync(doctorId, date);

            return Ok(reservations);
        }

        [Authorize(policy: "ConfirmedAccount")]
        public async Task<IActionResult> Create(string doctorId, DateTime date)
        {
            var model = await reservationServices.GetDoctorWithResPropertiesAsync(doctorId);
            var userName = User.Identity.Name;
            var result = await reservationServices.ReserveAsync(doctorId, date, userName);


            if (result == null)
            {
                ViewBag.Error = "An Error Occured while booking your reservation";
                return View("New", model);
            }

            try
            {
                var reservationEmailModel = await reservationServices.GetReservationEmailViewModelAsync(result.Id);
                await emailServices.SendReservationDoneEmailAsync(reservationEmailModel);
            }
            catch
            { }

            ViewBag.Success = "Reservation created successfully.";
            return View("New", model);

        }
    }
}
