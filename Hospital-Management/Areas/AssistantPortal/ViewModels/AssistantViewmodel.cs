using Hospital_Management.Models;
using Hospital_Management.Models.DataTypes;

namespace Hospital_Management.Areas.AssistantPortal.ViewModels
{
    public class AssistantViewmodel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DoctorId { get; set; }
        public Doctor doctor { get; set; }
        public string PatientName { get; set; }

        public string DoctorName { get; set; }
        

        public byte[] Img { get; set; }

        //public int specialityId { get; set; }
        //public Speciality speciality { get; set; }

        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public ReservationStatus ReservationStatus { get; set; }

        public List<Reservation> reservation { get; set; }
        public string PatientId { get; set; }
        public List<Patient> Patient { get; set; }
        public Reservation Res {  get; set; }

    }
}
