using Microsoft.AspNetCore.Mvc;
using Hospital_Management.Repository;
using Hospital_Management.Services;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Hospital_Management.Areas.AdminPortal.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Management.Areas.AdminPortal.Controllers
{
    [Area("AdminPortal")]
    [Authorize(Policy = "RequireAdminRole")]
    public class ReservationController(IUserServices<ApplicationUser> userServices, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : Controller
    {
        public IActionResult Index(int page = 1)
        {
            var doctorId = userManager.GetUserId(User);
            int pageSize = 10;
            var reservation = unitOfWork.ReservationRepository.GetPage(page);

            var reservationVM = new PaginationVM<Reservation>()
            {
                
                CurrentPage = page,
                TotalPages = unitOfWork.ReservationRepository.GetTotalPages(pageSize),
                Items = unitOfWork.ReservationRepository.GetPage(page)
            };
            return View("Index", reservationVM);
        }

        [HttpPost]
        public IActionResult Search(SearchVM<Reservation> searchVM)
        {
            searchVM.Items = unitOfWork.ReservationRepository.Search(searchVM.SearchString, searchVM.SearchString);
            return View("Search", searchVM);
        }

        public IActionResult Details(int id)
        {
            Reservation reservationModel = unitOfWork.ReservationRepository.GetById(id);

            if (reservationModel == null) return NotFound();
            return View("Details", reservationModel);
        }


    }
}
