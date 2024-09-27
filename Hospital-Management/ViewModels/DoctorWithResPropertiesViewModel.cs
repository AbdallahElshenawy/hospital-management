using Hospital_Management.Models.DataTypes;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.ViewModels
{
    public class DoctorWithResPropertiesViewModel : DoctorViewModel
    {
        public DaysOfWeek WorkingDays { get; set; }

        public int WorkingHours { get; set; }

        public int StartHour { get; set; }

        public int ExaminationsMinutes { get; set; }

        public decimal ExaminationFees { get; set; }
    }
}
