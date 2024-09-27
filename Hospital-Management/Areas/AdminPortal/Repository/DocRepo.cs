using Hospital_Management.Areas.AdminPortal.ViewModel;
using Hospital_Management.Data;
using Hospital_Management.Repository;

namespace Hospital_Management.Areas.AdminPortal.Repository
{
    public class DocRepo(ApplicationDbContext context)
    {
        public DoctorDetailsVM? BindGetDoctorDetails(string id)
        {
            return context.Doctors.Where(doc => doc.Id == id)
                .Select(doc => new DoctorDetailsVM
                {
                    DoctorId = doc.Id,
                    FirstName = doc.FirstName,
                    LastName = doc.LastName,
                    BirthDate = doc.BirthDate,
                    Address = doc.Address,
                    Img = doc.Img,
                    SpecialityName = doc.Speciality.Name,
                    Reservations = doc.Reservations.ToList(),
                    Articles = doc.Articles.ToList(),
                    Rates = doc.Rates.ToList(),
                    Assistants = doc.Assistants.ToList(),
                    ExaminationFees = doc.ExaminationFees,
                    ExaminationsMinutes = doc.ExaminationsMinutes,
                    StartHour = doc.StartHour,
                    WorkingDays = doc.WorkingDays,
                    WorkingHours = doc.WorkingHours,
                    SpecialityId = doc.SpecialityId,

                }
                ).FirstOrDefault();
        }
    }
}
