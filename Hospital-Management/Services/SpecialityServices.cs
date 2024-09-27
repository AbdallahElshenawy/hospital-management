using Hospital_Management.Data;
using Hospital_Management.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Services
{
    public class SpecialityServices(ApplicationDbContext context) : ISpecialityServices
    {
        public Task<SpecialityWithDoctorsViewModel?> GetById(int id)
        {
            var specialityWithDoctors = context.Specialities
                .Where(s => s.Id == id)
                .Select(s => new SpecialityWithDoctorsViewModel
                {
                    Name = s.Name,
                    Doctors = s.Doctors.Select(d => new DoctorViewModel
                    {
                        Id = d.Id,
                        Name = d.FullName,
                        Img = d.Img,
                        SpecialityName = d.Speciality.Name,
                        AvgRating = d.Rates.Count == 0 ? 0 : (int)d.Rates.Average(r => r.Value)
                    }).ToList()
                }).FirstOrDefaultAsync();

            return specialityWithDoctors;
        }

        public Task<List<SpecialityViewModel>> GetSpecialities()
        {
            var specialities = context.Specialities
                .Select(s => new SpecialityViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();

            return specialities;
        }
    }
}
