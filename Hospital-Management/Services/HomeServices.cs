using Hospital_Management.Data;
using Hospital_Management.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Services
{
    public class HomeServices(ApplicationDbContext context): IHomeServices
    {
        public async Task<HomeViewModel> GetHomeModel()
        {
            var model = new HomeViewModel();

            model.HomeArticles = await context.Articles
                .OrderBy(x => Guid.NewGuid())
                .Take(3)
                .ToListAsync();

            model.HomeDoctors = await context.Doctors
                .OrderBy(d => d.Rates.Average(r => r.Value))
                .Take(3)
                .Include(d => d.Speciality)
                .ToListAsync();

            model.HomeTestimonial = await context.Rates
                .OrderByDescending(r => r.Value)
                .Take(3)
                .Select(r => new RateViewModel
                {
                    PatientFullName = r.Patient.FullName,
                    PatientImg = r.Patient.Img,
                    Rate = r.Value,
                    Comment = r.Comment
                })
                .ToListAsync();

            model.Specialities = await context.Specialities
                .Take(4)
                .ToListAsync();

            return model;
        }
    }
}
