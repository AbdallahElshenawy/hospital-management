using Hospital_Management.Areas.AssistantPortal.Services;
using Hospital_Management.Areas.AssistantPortal.ViewModels;
using Hospital_Management.Data;
using Hospital_Management.Models;
using Hospital_Management.Models.DataTypes;
using Hospital_Management.Repository;
using Hospital_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Hospital_Management.Areas.AssistantPortal.Controllers
{
    [Area("AssistantPortal")]
    [Authorize(Policy = "RequireAssistantRole")]
    public class HomeController:Controller
        //(IUserServices<ApplicationUser> userServices, IUnitOfWork unitOfWork,IAssistantSer assistantSer) : Controller
    {
        ApplicationDbContext _context;
        IUserServices<ApplicationUser> userServices;
        IUnitOfWork unitOfWork;
        IAssistantSer assistantSer;
        public HomeController(IUserServices<ApplicationUser> userServices, IUnitOfWork unitOfWork, IAssistantSer assistantSer)
        {
            this.userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
            this.unitOfWork = unitOfWork;
            this.assistantSer = assistantSer ?? throw new ArgumentNullException(nameof(assistantSer));
            
        }

        public async Task<IActionResult> Index()
        {
            var user = await userServices.GetUserByUsernameAsync(User.Identity.Name);
            var userById = await userServices.GetUserByIdAsync(user.Id);

            Assistant assistant = unitOfWork.AssistantRepository.GetById(user.Id);
            AssistantViewmodel assisVM = new AssistantViewmodel();
            assisVM.Id = assistant.Id;
            assisVM.Name = assistant.FullName;
            assisVM.DoctorId = assistant.DoctorId;
            //assisVM.Img = assistant.Img;
            assisVM.reservation = assistantSer.GetReservationsOfDoctor(assistant.DoctorId);
            assisVM.doctor = unitOfWork.DoctorRepository.GetById(assistant.DoctorId);
            assisVM.ReservationId = assistantSer.GetReservationByDoctorId(assistant.DoctorId).Id;
            assisVM.PatientId = assistantSer.GetPatientIdByReservationId(assisVM.ReservationId);
            assisVM.PatientName = unitOfWork.PatientRepository.GetById(assisVM.PatientId).FullName;
            assisVM.ReservationDate = assistantSer.GetReservationByDoctorId(assistant.DoctorId).Date;
            assisVM.ReservationStatus = assistantSer.GetReservationByDoctorId(assistant.DoctorId).ReservationStatus;

            return View("Index", assisVM);
        }


        public async Task<IActionResult> EditDoctor(string id)
        {
            Doctor DocModel = unitOfWork.DoctorRepository.GetById(id);
            //AssistantViewmodel DocVM = new AssistantViewmodel(); 
            //DocVM.doctor.Id= DocModel.Id;
            //DocVM.doctor.FirstName = DocModel.FullName;
            //DocVM.doctor.WorkingDays = DocModel.WorkingDays;
            //DocVM.doctor.WorkingHours = DocModel.WorkingHours;
            //DocVM.doctor.StartHour = DocModel.StartHour;
            //DocVM.doctor.ExaminationsMinutes = DocModel.ExaminationsMinutes;
            //DocVM.doctor.ExaminationFees = DocModel.ExaminationFees;

            return View("EditDoctor", DocModel);
        }

        public async Task<IActionResult> EditReservation(int id)
        {
            Reservation ResModel = unitOfWork.ReservationRepository.GetById(id);

            AssistantViewmodel ResVM = new AssistantViewmodel();
            ResVM.ReservationId = ResModel.Id;
            ResVM.PatientId = ResModel.PatientId;
            ResVM.PatientName = unitOfWork.PatientRepository.GetById(ResVM.PatientId).FullName;
            ResVM.ReservationDate = ResModel.Date;
            
            ResVM.ReservationStatus = ResModel.ReservationStatus;
            ResVM.DoctorId = ResModel.DoctorId;
            ResVM.DoctorName = unitOfWork.DoctorRepository.GetById(ResVM.DoctorId).FullName;
            ViewBag.Statuses = Enum.GetValues(typeof(ReservationStatus))
            .Cast<ReservationStatus>()
            .Select(e => new SelectListItem
            {
                Value = ((int)e).ToString(),
                Text = e.GetType()
                        .GetMember(e.ToString())
                        .First()
                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                        .Cast<DisplayAttribute>()
                        .FirstOrDefault()?.Name ?? e.ToString()
            }).ToList();

            return View("EditReservation", ResVM);

        }

        public async Task<ActionResult> SaveDoctor(Doctor doctor)
        {
            Doctor DocModel = unitOfWork.DoctorRepository.GetById(doctor.Id);
            DocModel.Id = doctor.Id;
            DocModel.FirstName = doctor.FullName;
            DocModel.WorkingDays = doctor.WorkingDays;
            DocModel.WorkingHours = doctor.WorkingHours;
            DocModel.StartHour = doctor.StartHour;
            DocModel.ExaminationsMinutes = doctor.ExaminationsMinutes;
            DocModel.ExaminationFees = doctor.ExaminationFees;
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SaveReservation(AssistantViewmodel Res)
        {
            Reservation ResModel = unitOfWork.ReservationRepository.GetById(Res.ReservationId);
            ResModel.Id = Res.ReservationId;
            ResModel.Date = Res.ReservationDate;
            ResModel.ReservationStatus = Res.ReservationStatus;

            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}