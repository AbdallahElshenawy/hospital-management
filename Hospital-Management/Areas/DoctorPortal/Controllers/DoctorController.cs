using Hospital_Management.Data;
using Hospital_Management.Models;
using Hospital_Management.Models.DataTypes;
using Hospital_Management.Repository;
using Hospital_Management.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace Hospital_Management.Areas.DoctorPortal.Controllers
{
    [Area("DoctorPortal")]
    [Authorize(Policy = "RequireDoctorRole")]

    public class DoctorController(ApplicationDbContext context, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : Controller
    {
        public async Task<IActionResult> MyReservations(string searchString = "", string searchProperty = "Status", int page = 1)
        {
            var doctorId = userManager.GetUserId(User);
            int pageSize = 5;

            var reservationsQuery = context.Reservations
                .Include(r => r.Patient)
                .Where(r => r.DoctorId == doctorId);


            if (!string.IsNullOrEmpty(searchString))
            {
                switch (searchProperty)
                {
                    case "Status":
                        reservationsQuery = reservationsQuery.Where(r => r.ReservationStatus.ToString().Contains(searchString));
                        break;
                    case "PatientName":
                        reservationsQuery = reservationsQuery.Where(r => r.Patient.FullName.Contains(searchString));
                        break;
                }
            }

            int totalReservations = await reservationsQuery.CountAsync();

            int totalPages = (int)Math.Ceiling((double)totalReservations / pageSize);

            var pagedReservations = await reservationsQuery
                .OrderBy(r => r.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var reservationViewModels = pagedReservations.Select(r => new ReservationViewModel
            {
                Id = r.Id,
                PatientName = r.Patient != null ? r.Patient.FullName : "Patient",
                Date = r.Date,
                Status = Enum.GetName(typeof(ReservationStatus), r.ReservationStatus)
            }).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchString = searchString;
            ViewBag.SearchProperty = searchProperty;

            return View(reservationViewModels);
        }
        public async Task<IActionResult> PatientRecords()
        {
            var doctorId = userManager.GetUserId(User);

            var records = unitOfWork.RecordRepository.GetByDoctorId(doctorId).ToList();

            var patientId = records.Select(r => r.PatientId).Distinct().ToList();

            var patient = await context.Patients
                .Where(p => patientId.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id, p => p.FullName);

            var recordViewModels = records.Select(r => new RecordViewModel
            {
                Id = r.Id,
                Description = r.Description,
                Diagnosis = r.Diagnosis,
                Prescription = r.Prescription,
                Notes = r.Notes,
                Date = r.Date,
                PatientName = patient.ContainsKey(r.PatientId) ? patient[r.PatientId] : "Unknown",
                DoctorId = r.DoctorId
            }).ToList();

            return View(recordViewModels);
        }
        /*///////
         
         
         
         
         cvdsds
         
         */
        private async Task<PatientRecordsVM> CreatePatientRecordsVMAsync(string patientId, string doctorId)
        {
            if (string.IsNullOrEmpty(patientId) || string.IsNullOrEmpty(doctorId))
            {
                throw new ArgumentException("PatientId or DoctorId cannot be null or empty");
            }

            var patient = await context.Patients.FindAsync(patientId);
            if (patient == null)
            {
                throw new ArgumentException($"Patient with ID {patientId} not found");
            }

            var records = await context.Records
                .Where(r => r.PatientId == patientId && r.DoctorId == doctorId)
                .ToListAsync();

            var patientRecordsVM = new PatientRecordsVM
            {
                PatientId = patient.Id,
                PatientName = patient.FullName,
                Records = records.Select(r => new RecordViewModel
                {
                    Date = r.Date,
                    Description = r.Description,
                    Diagnosis = r.Diagnosis,
                    Prescription = r.Prescription,
                    Notes = r.Notes
                }).ToList()
            };

            return patientRecordsVM;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecord(RecordViewModel recordModel)
        {
            if (!ModelState.IsValid)
            {
                var patientRecordsVM = await CreatePatientRecordsVMAsync(recordModel.PatientId, userManager.GetUserId(User)) ?? new PatientRecordsVM();
                return View("~/Areas/DoctorPortal/Views/Shift/ShiftPatientRecords.cshtml", patientRecordsVM);
            }

            var doctorId = userManager.GetUserId(User);
            var reservation = await context.Reservations
                .FirstOrDefaultAsync(r => r.DoctorId == doctorId && r.ReservationStatus == ReservationStatus.Pending);

            if (reservation == null)
            {
                ModelState.AddModelError("PatientId", "Patient not found or does not have a pending reservation with the current doctor");
                var patientRecordsVM = await CreatePatientRecordsVMAsync(recordModel.PatientId, doctorId) ?? new PatientRecordsVM();
                return View("~/Areas/DoctorPortal/Views/Shift/ShiftPatientRecords.cshtml", patientRecordsVM);
            }

            var record = new Record
            {
                Description = recordModel.Description,
                Diagnosis = recordModel.Diagnosis,
                Prescription = recordModel.Prescription,
                Notes = recordModel.Notes,
                Date = DateTime.Now,
                PatientId = reservation.PatientId,
                DoctorId = doctorId
            };

            context.Records.Add(record);
            reservation.ReservationStatus = ReservationStatus.Done;
            await context.SaveChangesAsync();

            var patientRecordsVMAfterSave = await CreatePatientRecordsVMAsync(record.PatientId, doctorId) ?? new PatientRecordsVM();
            return View("~/Areas/DoctorPortal/Views/Doctor/PatientRecords.cshtml", patientRecordsVMAfterSave);
        }
        [HttpGet]
        public async Task<IActionResult> EditRecord(int id)
        {
            var record = unitOfWork.RecordRepository.GetById(id);

            if (record == null) return NotFound();

            var recordVM = new RecordViewModel
            {
                Id = record.Id,
                Description = record.Description,
                Diagnosis = record.Diagnosis,
                Prescription = record.Prescription,
                Notes = record.Notes,
                Date = record.Date,
                PatientId = record.PatientId,
                DoctorId = record.DoctorId,

            };
            ViewData["Patients"] = new SelectList(unitOfWork.PatientRepository.GetAll(), "Id", "FullName", recordVM.PatientId);
            return View("EditRecord", recordVM);
        }

        [HttpPost]
        public IActionResult SaveEditRecord(RecordViewModel recordModel)
        {
          
            if (ModelState.IsValid)
            {
                var record = unitOfWork.RecordRepository.GetById(recordModel.Id);
                if (record == null) return NotFound();
                record.Description = recordModel.Description;
                record.Diagnosis = recordModel.Diagnosis;
                record.Prescription = recordModel.Prescription;
                record.Notes = recordModel.Notes;
                record.Date = recordModel.Date;
                record.PatientId = recordModel.PatientId;

                unitOfWork.RecordRepository.Update(record);
                unitOfWork.Save();
                return RedirectToAction("PatientRecords");
            }
            ViewData["Patients"] = new SelectList(unitOfWork.PatientRepository.GetAll(), "Id", "FullName", recordModel.PatientId);
            return View("EditRecord", recordModel);
        }

        #region Articles
        public IActionResult Articles(string searchString = "", string searchProperty = "Title", int page = 1)
        {
            var doctorId = userManager.GetUserId(User);
            int pageSize = 5;

            var allArticles = unitOfWork.ArticleRepository.Search(searchString, searchProperty)
                                    .Where(a => a.DoctorId == doctorId);

            int totalPages = (int)Math.Ceiling((double)allArticles.Count() / pageSize);

            var pagedArticles = allArticles.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var articleViewModels = pagedArticles.Select(a => new ArticleSimpleViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                DateTime = a.DateTime,
                DoctorId = a.DoctorId
            }).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchString = searchString;
            ViewBag.SearchProperty = searchProperty;

            return View("Articles", articleViewModels);
        }


        [HttpGet]
        public IActionResult CreateArticle()
        {
            return View("CreateArticle");
        }

        [HttpPost]
        public IActionResult Save(ArticleSimpleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = new Article
                {
                    Title = model.Title,
                    Content = model.Content,
                    DateTime = model.DateTime,
                    DoctorId = userManager.GetUserId(User)
                };
                unitOfWork.ArticleRepository.Insert(article);
                unitOfWork.Save();
                return View("Articles");
            }

            return View("CreateArticle", model);
        }
        [HttpGet]
        public IActionResult EditArticle(int id)
        {

            var article = unitOfWork.ArticleRepository.GetById(id);


            if (article == null) return NotFound();

            var articleVM = new ArticleSimpleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                DateTime = article.DateTime,
                DoctorId = article.DoctorId
            };

            return View("EditArticle", articleVM);
        }

        [HttpPost]
        [Route("DoctorPortal/Doctor/SaveEditArticle")]

        public IActionResult SaveEditArticle(ArticleSimpleViewModel articleVM)
        {
            if (ModelState.IsValid)
            {
                var article = unitOfWork.ArticleRepository.GetById(articleVM.Id);
                if (article == null) return NotFound();
                article.Title = articleVM.Title;
                article.Content = articleVM.Content;
                article.DateTime = articleVM.DateTime;
                unitOfWork.ArticleRepository.Update(article);
                unitOfWork.Save();
                return RedirectToAction("Articles");

            }

            return View("EditArticle", articleVM);
        }


        #endregion

       
        public IActionResult ViewRates(string searchString = "", string searchProperty = "Value", int page = 1)
        {
            int pageSize = 5;

            var doctorId = userManager.GetUserId(User);

            var allRates = context.Rates
                .Include(r => r.Patient)
                .Where(r => r.DoctorId == doctorId)
                .ToList();

            var filteredRates = allRates.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                if (searchProperty == "Value")
                {
                    filteredRates = filteredRates.Where(r => r.Value.ToString().Contains(searchString));
                }
                else if (searchProperty == "Comment")
                {
                    filteredRates = filteredRates.Where(r => r.Comment.Contains(searchString));
                }
                else if (searchProperty == "PatientName")
                {
                    filteredRates = filteredRates.Where(r => r.Patient.FullName.Contains(searchString));
                }
            }

            int totalPages = (int)Math.Ceiling((double)filteredRates.Count() / pageSize);

            var rates = filteredRates
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var rateViewModels = rates.Select(r => new RateVM
            {
                Id = r.Id,
                Value = r.Value,
                Comment = r.Comment ?? string.Empty,
                PatientName = r.Patient?.FullName ?? string.Empty
            }).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchString = searchString;
            ViewBag.SearchProperty = searchProperty;

            return View("ViewRates", rateViewModels);
        }



    }
}
