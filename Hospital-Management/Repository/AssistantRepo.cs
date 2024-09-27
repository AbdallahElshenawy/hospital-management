using Hospital_Management.Data;
using Hospital_Management.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hospital_Management.Repository
{
    public class AssistantRepo(ApplicationDbContext context) : IAssistantRepo
    {
        public void Delete(string id)
        {
            var assistant = context.Assistants.Find(id);
            if (assistant != null)
            {
                context.Assistants.Remove(assistant);
            }
        }

        public List<Assistant> GetAll()
        {
            return context.Assistants.Include(a => a.Doctor).ToList();
        }

        public Assistant GetById(string id)
        {
            return context.Assistants.Include(a => a.Doctor)
                .FirstOrDefault(a => a.Id == id.ToString());
        }

        public List<Assistant> GetPage(int page)
        {
            int pageSize = 5;

            return context.Assistants.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetTotalPages(int pageSize)
        {
            return (int)Math.Ceiling((double)context.Assistants.Count() / pageSize);
        }

        public void Insert(Assistant entity)
        {
            context.Assistants.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<Assistant> Search(string searchString, string searchProperty)
        {
            switch (searchProperty)
            {
                case "FullName":
                    return context.Assistants.Where(a =>a.FullName.Contains(searchString)).ToList();
                case "Doctor":
                    return context.Assistants.Where(a => a.Doctor.FullName.Contains(searchString)).ToList();

                default:
                    return context.Assistants.Where(a => a.FirstName.Contains(searchString)).ToList();
            }
        }

        public void Update(Assistant entity)
        {
            context.Assistants.Update(entity);
        }
    }
}
