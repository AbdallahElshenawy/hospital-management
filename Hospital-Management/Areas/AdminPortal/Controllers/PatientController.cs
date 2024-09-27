using Hospital_Management.Areas.AdminPortal.ViewModel;
using Hospital_Management.Models;
using Hospital_Management.Repository;
using Hospital_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Areas.AdminPortal.Controllers
{
    [Area("AdminPortal")]
    [Authorize(Policy = "RequireAdminRole")]
    public class PatientController(IUserServices<ApplicationUser> userServices, IUnitOfWork unitOfWork) : Controller
    {
        
        public ActionResult Index(int page=1)
        {
            int pageSize = 10;
            var patients = unitOfWork.PatientRepository.GetPage(page);

            var patientVM = new PaginationVM<Patient>()
            {
                CurrentPage = page,
                TotalPages = unitOfWork.PatientRepository.GetTotalPages(pageSize),
                Items = patients,
            };

            return View("Index", patientVM);
        }

        [HttpPost]
        public IActionResult Search(SearchVM<Patient> searchVM)
        {
            SearchVM<Patient> patientSearchVM;

            searchVM.Items = unitOfWork.PatientRepository.Search(searchVM.SearchString, searchVM.SearchString);

            return View("Search", searchVM);
        }

        // GET: PatientController/Details/5
        public IActionResult Details(string id)
        {
            Patient patientModel = unitOfWork.PatientRepository.GetById(id);

            if (patientModel == null) return NotFound();
            return View("Details", patientModel);
        }

        // GET: PatientController/Create
        public ActionResult Add()
        {
            return View("Add");
        }

        // POST: PatientController/Create
        [HttpPost]
        public async Task<IActionResult> SaveAdd(Patient patientFromReq, IFormFile imgFile)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", patientFromReq);
            }

            var patient = new Assistant()
            {
                FirstName = patientFromReq.FirstName,
                LastName = patientFromReq.LastName,
                BirthDate = patientFromReq.BirthDate,
                Address = patientFromReq.Address,
                PhoneNumber = patientFromReq?.PhoneNumber,
               
            };

            userServices.Register(patient, "patient1234", "Patient");

            return RedirectToAction("Index");///////////////
        }

        // GET: PatientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
