using Hospital_Management.Data;
using Hospital_Management.Models;

namespace Hospital_Management.Areas.AssistantPortal.Services
{
    public class AssistantSer(ApplicationDbContext context):IAssistantSer
    {
        public Reservation GetReservationByDoctorId(string Docid)
        {
            return context.Reservations.FirstOrDefault(e => e.DoctorId == Docid);
        }

        public List<Reservation> GetReservationsOfDoctor(string doctorId)
        {
            return context.Reservations.Where(e => e.DoctorId == doctorId).ToList();
        }

        public string GetPatientIdByReservationId(int ResId)
        {
            return context.Reservations.FirstOrDefault(o => o.Id == ResId).PatientId;
        }

        public List<Patient> GetPatientsByDoctorId(string doctorId)
        {

            var patients = context.Reservations
                .Where(r => r.DoctorId == doctorId)
                .Select(r => r.Patient)
                .ToList();

            return patients;
        }

        public Patient GetPatientByDoctorId(string docId, string PatientId)
        {
            var patients = context.Reservations
                .Where(r => r.DoctorId == docId)
                .Select(r => r.Patient)
                .FirstOrDefault(e => e.Id == PatientId);

            return patients;
        }
    }
}
