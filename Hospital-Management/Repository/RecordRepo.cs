using Hospital_Management.Data;
using Hospital_Management.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Hospital_Management.Repository
{
    public class RecordRepo(ApplicationDbContext context) : IRecordRepo
    {
        public void Delete(int id)
        {
            var record = context.Records.Find(id);
            if (record != null)
            {
                context.Records.Remove(record);
            }
        }
        public List<Record> GetByDoctorId(string doctorId)
        {
            return context.Records.Where(r => r.DoctorId == doctorId).ToList();
        }

        public List<Record> GetAll()
        {
            return context.Records.Include(r => r.Patient).Include(r => r.Doctor).ToList();
        }

        public Record GetById(int id)
        {
            return context.Records.Include(r => r.Patient).Include(r => r.Doctor)
                   .FirstOrDefault(r => r.Id == id);
        }

        public List<Record> GetPage(int page)
        {
            int pageSize = 5;

            return context.Records.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetTotalPages(int pageSize)
        {
            return (int)Math.Ceiling((double)context.Records.Count() / pageSize);
        }

        public void Insert(Record record)
        {
            context.Records.Add(record);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<Record> Search(string searchString, string searchProperty)
        {
            switch (searchProperty)
            {
                case "Description":
                    return context.Records.Where(r => r.Description.Contains(searchString)).ToList();
                case "Diagnosis":
                    return context.Records.Where(r => r.Diagnosis.Contains(searchString)).ToList();
                case "Doctor":
                    return context.Records.Where(r => r.Doctor.FullName.Contains(searchString)).ToList();
                case "Patient":
                    return context.Records.Where(r => r.Patient.FullName.Contains(searchString)).ToList();
                default:
                    return context.Records.Where(r => r.Prescription.Contains(searchString)).ToList();
            }
        }

        public void Update(Record entity)
        {
            context.Records.Update(entity);
        }
    }
}
