using Hospital_Management.Areas.AdminPortal.ViewModel;
using Hospital_Management.Models;
using Hospital_Management.Repository;
using Hospital_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Areas.AdminPortal.Controllers
{
    [Area("AdminPortal")]
    [Authorize(Policy = "RequireAdminRole")]
    public class DoctorController(IUserServices<ApplicationUser> userServices ,IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : Controller
    {
        //private readonly object webHostEnv;

        //public IActionResult Index(int page=1)
        //{
        //    int pageSize = 10;

        //    var doctorsVM = new PaginationVM<DoctorWithSpecialityVM>()
        //    {
        //        CurrentPage = page,
        //        TotalPages = unitOfWork.DoctorRepository.GetTotalPages(pageSize),
        //        Items = unitOfWork.DoctorRepository.GetPage(page)
        //    };
        //    return View("Index", doctorsVM);
        //}

        public IActionResult Index(int page = 1)
        {
            int pageSize = 10;

            var doctors = unitOfWork.DoctorRepository.GetPage(page);
            //var spec = unitOfWork.SpecialityRepository.GetAll();
            
            var doctorVMs = doctors.Select(doctor => new DoctorWithSpecialityVM
            {
                DoctorId = doctor.Id,
                FirstName = doctor.FirstName + " " + doctor.LastName,
                SpecialityId = doctor.SpecialityId,
                BirthDate = doctor.BirthDate,
                Address = doctor.Address,
                
            }).ToList();

            // Create a PaginationVM<DoctorWithSpecialityVM> instance
            var paginationVM = new PaginationVM<DoctorWithSpecialityVM>()
            {
                CurrentPage = page,
                TotalPages = unitOfWork.DoctorRepository.GetTotalPages(pageSize),
                Items = doctorVMs
                
            };

            return View("Index", paginationVM);
        }

        [HttpPost]
        public IActionResult Search(SearchVM<DoctorWithSpecialityVM> searchVM)
        {
            

            searchVM.Items = unitOfWork.DoctorRepository.Search(searchVM.SearchString, searchVM.SearchProperty)
                .Select(doctor => new DoctorWithSpecialityVM
                {
                    DoctorId = doctor.Id,
                    FirstName = doctor.FirstName + " " + doctor.LastName,
                    SpecialityId = doctor.SpecialityId,
                    BirthDate = doctor.BirthDate,
                    Address = doctor.Address,
                }).ToList(); 

            return View("Search", searchVM);
        }

        public IActionResult Details(string id)
        {
            //create BindGetDoctorDetails DoctorDetailsVM doctorVM = unitOfWork.DoctorRepository.BindGetDoctorDetails(id);
            // CourseDetailsVM courseVM = CourseRepo.BindGetCourseDetails(id);

            Doctor doctorModel = unitOfWork.DoctorRepository.GetById(id);

            DoctorDetailsVM doctorVM = new DoctorDetailsVM()
            {
                FirstName = doctorModel.FirstName,
                LastName = doctorModel.LastName,
                Address = doctorModel.Address,
                BirthDate= doctorModel.BirthDate,
                Img = doctorModel.Img,
                SpecialityName = doctorModel.Speciality?.Name,
                SpecialityId = doctorModel.SpecialityId,
                Rates = doctorModel.Rates,
                Articles    = doctorModel.Articles,
                Reservations = doctorModel.Reservations,
                Assistants = doctorModel.Assistants,
                WorkingHours = doctorModel.WorkingHours,
                WorkingDays = doctorModel.WorkingDays,
                StartHour = doctorModel.StartHour,
                ExaminationsMinutes = doctorModel.ExaminationsMinutes,
                ExaminationFees = doctorModel.ExaminationFees,
            };
            

            if (doctorModel == null) return NotFound();
            return View("Details", doctorVM);
        }

        [HttpGet]
        public IActionResult Add()
        {
            //var addDoctorVM = new DoctorDetailsVM();

            DoctorWithSpecialityVM addDoctorVM = new DoctorWithSpecialityVM()
            {
                Specialities = unitOfWork.SpecialityRepository.GetAll().Select(spec => new SelectListItem
                {
                    Value = spec.Id.ToString(),
                    Text = spec.Name,
                }).ToList()
            };
            return View("Add", addDoctorVM);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAdd(DoctorWithSpecialityVM doctorVMFromReq, IFormFile imgFile)
        {
            if (!ModelState.IsValid)
            {
                doctorVMFromReq.Specialities = unitOfWork.SpecialityRepository.GetAll().Select(spec => new SelectListItem
                {
                    Value = spec.Id.ToString(),
                    Text = spec.Name,
                }).ToList(); ;
                
                return View("Add", doctorVMFromReq);
            }

            var doctor = new Doctor()
            {
                FirstName = doctorVMFromReq.FirstName,
                LastName = doctorVMFromReq.LastName,
                BirthDate = doctorVMFromReq.BirthDate,
                Address = doctorVMFromReq.Address,
                SpecialityId = doctorVMFromReq.SpecialityId,
                Img = await UploadImage(imgFile),
            };


            //unitOfWork.DoctorRepository.Insert(doctor);
            //unitOfWork.DoctorRepository.Save();
            userServices.Register(doctor, "doctor1234", "Doctor");
                ///////////////
           
            return RedirectToAction("Index");///////////////
        }

        //Doctor/Edit/Id=1
        [HttpGet]
        public IActionResult Edit(string id)
        {
            //var user = await userServices.GetUserByUsernameAsync(User.Identity.Name);
            //var userById = await userServices.GetUserByIdAsync(user.Id);
            var doctorByIdFromRepo = unitOfWork.DoctorRepository.GetById(id);

            if (doctorByIdFromRepo == null)
            {
                // Handle the case where the doctor was not found
                return NotFound();
            }

            var editDoctorVM = new DoctorWithSpecialityVM()
            {
                DoctorId = doctorByIdFromRepo.Id,
                FirstName = doctorByIdFromRepo.FirstName,
                LastName = doctorByIdFromRepo.LastName,
                BirthDate = doctorByIdFromRepo.BirthDate,
                Address = doctorByIdFromRepo.Address,
                Img = doctorByIdFromRepo?.Img,
                SpecialityId = doctorByIdFromRepo.SpecialityId,
                
                Specialities = unitOfWork.SpecialityRepository.GetAll().Select(spec => new SelectListItem
                {
                    Value = spec.Id.ToString(),
                    Text = spec.Name
                }).ToList()
            };

            return View("Edit", editDoctorVM);
        }
        
        //doctor/saveEdit/object request
        [HttpPost]
        public IActionResult SaveEdit(DoctorWithSpecialityVM editDocVMFromReq)
        {
            if (!ModelState.IsValid)
            {
                editDocVMFromReq.Specialities = unitOfWork.SpecialityRepository.GetAll().Select(spec => new SelectListItem
                {
                    Value = spec.Id.ToString(),
                    Text = spec.Name
                }).ToList();

                return View("Edit", editDocVMFromReq);
            }


            Doctor doctor = new()
            {
                Id = editDocVMFromReq.DoctorId,
                FirstName = editDocVMFromReq.FirstName,
                LastName = editDocVMFromReq.LastName,
                BirthDate = editDocVMFromReq.BirthDate,
                Address = editDocVMFromReq.Address,
                Img = editDocVMFromReq.Img,
                SpecialityId = editDocVMFromReq.SpecialityId,
            };


            unitOfWork.DoctorRepository.Update(doctor);

            unitOfWork.DoctorRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                var doctor = unitOfWork.DoctorRepository.GetById(id);
                if (doctor == null)
                {
                    return StatusCode(404, new { message = "Doctor not found." });
                }

                unitOfWork.DoctorRepository.Delete(id);

                unitOfWork.DoctorRepository.Save();

                return StatusCode(201, new { message = "Course successfully removed." });
            }
            catch
            {
                return StatusCode(500, $"An error occured while removing the Course.");
            }
        }

        private async Task<byte[]> UploadImage(IFormFile imgFile)
        {
            string imgPath;

            if (imgFile != null)
            {
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                var fileName = timestamp + "-" + imgFile.FileName;
                imgPath = Path.Combine(webHostEnvironment.WebRootPath, "Images", fileName);

                try
                {
                    //using (var fileStream = new FileStream(imgPath, FileMode.Create))
                    //{
                    //    await imgFile.CopyToAsync(fileStream);
                    //}
                    //return $"/Images/{fileName}";
                    using (var memoryStream = new MemoryStream())
                    {
                        await imgFile.CopyToAsync(memoryStream);
                        return memoryStream.ToArray(); // Return the byte array
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        private bool RemoveImage(string imgPath)
        {
            bool status = false;

            if (imgPath != null)
            {
                try
                {
                    var fullPath = Path.Combine(webHostEnvironment.WebRootPath, "Images", imgPath.Split("/")[^1]);
                    System.IO.File.Delete(fullPath);
                    status = true;
                }
                catch
                {
                    status = false;
                }
            }

            return status;
        }


    }
}
