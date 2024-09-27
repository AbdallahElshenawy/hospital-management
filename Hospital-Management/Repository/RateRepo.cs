using Hospital_Management.Data;
using Hospital_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Repository
{
    public class RateRepo(ApplicationDbContext context) : IRateRepo
    {
        public void Delete(int id)
        {
            var rate = context.Rates.Find(id);
            if (rate != null)
            {
                context.Rates.Remove(rate);
            }
        }

        public List<Rate> GetAll()
        {
            return context.Rates.Include(r => r.Doctor).Include(r => r.Patient).ToList();
        }

        public Rate GetById(int id)
        {
            return context.Rates.Include(r => r.Doctor).Include(r => r.Patient)
                  .FirstOrDefault(r => r.Id == id);
        }

        public List<Rate> GetPage(int page)
        {
            int pageSize = 5;

            return context.Rates.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetTotalPages(int pageSize)
        {
            return (int)Math.Ceiling((double)context.Rates.Count() / pageSize);
        }

        public void Insert(Rate rate)
        {
            context.Rates.Add(rate);
        }
        public List<Rate> GetByDoctorId(string doctorId)
        {
            return context.Rates.Where(r => r.DoctorId == doctorId).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<Rate>Search(string searchString, string searchProperty)
        {
            switch (searchProperty)
            {
                case "Value":
                    return context.Rates.Where(r => r.Value.ToString().Contains(searchString)).ToList();
                case "Doctor":
                    return context.Rates.Where(r => r.Doctor.FullName.Contains(searchString)).ToList();
                case "Patient":
                    return context.Rates.Where(r => r.Patient.FullName.Contains(searchString)).ToList();
                default:
                    return context.Rates.Where(r => r.Comment.Contains(searchString)).ToList();
            }
        }
        public void Update(Rate entity)
        {
            context.Rates.Update(entity);
        }
    }
}
