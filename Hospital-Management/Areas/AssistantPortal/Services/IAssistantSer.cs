using Hospital_Management.Models;

namespace Hospital_Management.Areas.AssistantPortal.Services
{
    public interface IAssistantSer
    {
        public Reservation GetReservationByDoctorId(string Docid);
        public List<Reservation> GetReservationsOfDoctor(string doctorId);

        public string GetPatientIdByReservationId(int ResId);
        public List<Patient> GetPatientsByDoctorId(string doctorId);
        public Patient GetPatientByDoctorId(string docId, string PatientId);
    }
}
