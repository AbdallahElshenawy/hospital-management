using Hospital_Management.Areas.AdminPortal.ViewModel;
using Hospital_Management.Models;
using Hospital_Management.Repository;
using Hospital_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital_Management.Areas.AdminPortal.Controllers
{
    [Area("AdminPortal")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AssistantController(IUserServices<ApplicationUser> userServices, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : Controller
    {
        public IActionResult Index(int page=1)
        {
            int pageSize = 10;
            var assistants = unitOfWork.AssistantRepository.GetPage(page);

            var assistantsVM = new PaginationVM<Assistant>()
            {
                CurrentPage = page,
                TotalPages = unitOfWork.AssistantRepository.GetTotalPages(pageSize),
                Items = assistants,
            };

            return View("Index",assistantsVM);
        }

        [HttpPost]
        public IActionResult Search(SearchVM<Assistant> searchVM)
        {
            searchVM.Items = unitOfWork.AssistantRepository.Search(searchVM.SearchString, searchVM.SearchString);
            return View("Search", searchVM);
        }

        public IActionResult Details(string id)
        {
            //create BindGetDoctorDetails DoctorDetailsVM doctorVM = unitOfWork.DoctorRepository.BindGetDoctorDetails(id);

            Assistant assistantModel = unitOfWork.AssistantRepository.GetById(id);

            if (assistantModel == null) return NotFound();
            return View("Details", assistantModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            
            return View("Add");
        }

        [HttpPost]
        public async Task<IActionResult> SaveAdd(Assistant assistantVMFromReq, IFormFile imgFile)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", assistantVMFromReq);
            }

            var assistant = new Assistant()
            {
                FirstName = assistantVMFromReq.FirstName,
                LastName = assistantVMFromReq.LastName,
                BirthDate = assistantVMFromReq.BirthDate,
                Address = assistantVMFromReq.Address,
                DoctorId = assistantVMFromReq.DoctorId,
                Img = await UploadImage(imgFile),
            };


            //unitOfWork.DoctorRepository.Insert(doctor);
            //unitOfWork.DoctorRepository.Save();
            userServices.Register(assistant, "assistant1234", "Assistant");

            return RedirectToAction("Index");///////////////
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            Assistant assistantModel = unitOfWork.AssistantRepository.GetById(id);
            var assistantVM = new Assistant()
            {
                FirstName = assistantModel.FirstName,
                LastName = assistantModel.LastName,
                BirthDate = assistantModel.BirthDate,
                Address = assistantModel.Address,
                DoctorId = assistantModel.DoctorId,
            };

            if (assistantVM == null)
            {
                return NotFound();
            }

            return View("Edit", assistantModel);
        }


        [HttpPost]
        public IActionResult SaveEdit(Assistant editassistantFromReq)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", editassistantFromReq);
            }

            Assistant assistant = new()
            {
                Id = editassistantFromReq.Id,
                FirstName = editassistantFromReq.FirstName,
                LastName = editassistantFromReq.LastName,
                BirthDate = editassistantFromReq.BirthDate,
                Address = editassistantFromReq.Address,
                Img = editassistantFromReq.Img,
                DoctorId = editassistantFromReq.DoctorId,
            };

            //unitOfWork.AssistantRepository.Update(assistant);
            //unitOfWork.AssistantRepository.Save();
            userServices.Register(assistant, "assistant1234", "Assitant");
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                var assistant = unitOfWork.AssistantRepository.GetById(id);
                if (assistant == null)
                {
                    return StatusCode(404, new { message = "Course not found." });
                }

                unitOfWork.AssistantRepository.Delete(id);

                unitOfWork.AssistantRepository.Save();

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
