using Hospital_Management.Models;
using Hospital_Management.Models.DataTypes;
using Hospital_Management.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Areas.AdminPortal.ViewModel
{
    public class DoctorDetailsVM
    {
        public string? DoctorId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("Name")]
        public string FullName => $"{FirstName} {LastName}";

        public int SpecialityId { get; set; }
        public string? SpecialityName { get; set; }

        public virtual List<SelectListItem> Specialities { get; set; }

        public virtual Speciality Speciality { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Address { get; set; }

        public byte[]? Img { get; set; }

        //------------------- More Details ------------------------
        public virtual List<Reservation> Reservations { get; set; }

        public virtual List<Assistant> Assistants { get; set; }

        public virtual List<Rate> Rates { get; set; }

        public virtual List<Article> Articles { get; set; }

        public DaysOfWeek WorkingDays { get; set; }

        public int WorkingHours { get; set; }

        [Range(0, 23)]
        public int StartHour { get; set; }

        public int ExaminationsMinutes { get; set; }

        public decimal ExaminationFees { get; set; }
    }
}
