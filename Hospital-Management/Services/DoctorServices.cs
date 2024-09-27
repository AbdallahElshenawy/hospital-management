using Hospital_Management.Data;
using Hospital_Management.Models;
using Hospital_Management.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Services
{
    public class DoctorServices(ApplicationDbContext context) : IDoctorServices
    {
        public async Task<DoctorViewModel> GetById(string id)
        {
            var doctor = await context.Doctors.FindAsync(id);

            return new DoctorViewModel
            {
                Id = doctor.Id,
                Name = doctor.FullName,
                Img = doctor.Img,
                SpecialityName = doctor.Speciality.Name,
                AvgRating = doctor.Rates.Count == 0 ? 0 : (int)doctor.Rates.Average(r => r.Value)
            };
        }

        public async Task<PaginationViewModel<DoctorViewModel>> GetDoctors(int page, int pageSize)
        {
            var doctors = await context.Doctors
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new DoctorViewModel
                {
                    Id = d.Id,
                    Name = d.FullName,
                    Img = d.Img,
                    SpecialityName = d.Speciality.Name,
                    AvgRating = d.Rates.Count == 0 ? 0 : (int)d.Rates.Average(r => r.Value)
                }).ToListAsync();

            var count = await context.Doctors.CountAsync();

            return new PaginationViewModel<DoctorViewModel>
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Items = doctors
            };
        }

        public async Task<PaginationViewModel<DoctorViewModel>> Search(int page, int pageSize, string searchString, string? searchProperty = null)
        {
            IQueryable<Doctor> searchQuery;
            switch (searchProperty)
            {
                case "Speciality":
                    searchQuery = context.Doctors
                        .Where(d => d.Speciality.Name.Contains(searchString));
                    break;
                default: // Name
                    searchQuery = context.Doctors
                        .Where(d => d.FirstName.Contains(searchString) || d.LastName.Contains(searchString));
                    break;
            }

            var doctors = await searchQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new DoctorViewModel
                {
                    Id = d.Id,
                    Name = d.FullName,
                    Img = d.Img,
                    SpecialityName = d.Speciality.Name,
                    AvgRating = d.Rates.Count == 0 ? 0 : (int)d.Rates.Average(r => r.Value)
                }).ToListAsync();

            var count = await searchQuery.CountAsync();

            return new PaginationViewModel<DoctorViewModel>
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Items = doctors
            };
        }
    }
}
