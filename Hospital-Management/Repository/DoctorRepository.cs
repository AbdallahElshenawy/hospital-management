using Hospital_Management.Data;
using Hospital_Management.Models;
using Hospital_Management.Models.DataTypes;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Repository
{
    public class DoctorRepo(ApplicationDbContext context) : IDoctorRepo
    {
        public void Delete(string id)
        {
            context.Doctors.Remove(context.Doctors.Find(id));
        }

        public List<Doctor> GetAll()
        {
            return context.Doctors.ToList();
        }

        public Doctor GetById(string id)
        {
            return context.Doctors.Include(d=>d.Reservations).Include(d=>d.Articles).Include(d => d.Assistants).Include(d => d.Speciality).Include(d => d.Rates).FirstOrDefault(d=>d.Id==id);
        }

        public List<Doctor> GetPage(int page)
        {
             int pageSize = 10;
            return context.Doctors.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetTotalPages(int pageSize)
        {
            return (int)Math.Ceiling((double)context.Doctors.Count() / pageSize);
        }

        public void Insert(Doctor entity)
        {
            context.Doctors.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<Doctor> Search(string searchString, string searchProperty)
        {
            switch (searchProperty)
            {
                case "WorkingDays":
                    DaysOfWeek WorkingDays;
                    DaysOfWeek.TryParse(searchString, out WorkingDays);
                    return context.Doctors.Where(doc => doc.WorkingDays== WorkingDays).ToList();
                case "WorkingHours":
                    int Hours;
                    int.TryParse(searchString, out Hours);
                    return context.Doctors.Where(docs => docs.WorkingHours == Hours).ToList();
                case "Speciality":
                    return context.Doctors.Where(docs => docs.Speciality.Name==searchString).ToList();
                case "StartHour":
                    int StartHour;
                    int.TryParse(searchString, out StartHour);
                    return context.Doctors.Where(docs => docs.StartHour == StartHour).ToList();
                default: // Otherwise, search by name.
                    return context.Doctors.Where(docs => docs.FirstName.Contains(searchString) || 
                    docs.LastName.Contains(searchString)).ToList();
            }
        }

        public void Update(Doctor entity)
        {
            context.Doctors.Update(entity);
        }
    }
}
