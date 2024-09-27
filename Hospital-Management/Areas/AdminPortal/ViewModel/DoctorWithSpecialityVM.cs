using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Hospital_Management.Areas.AdminPortal.ViewModel
{
    public class DoctorWithSpecialityVM
    {
        public string DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("Name")]
        public string FullName => $"{FirstName} {LastName}";

        public int SpecialityId { get; set; }
        public virtual List<SelectListItem>? Specialities { get; set; }

        public virtual Speciality? Speciality { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Address { get; set; }

        public byte[]? Img { get; set; }
    }
}
