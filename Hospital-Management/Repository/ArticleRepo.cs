using Hospital_Management.Data;
using Hospital_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Repository
{
    public class ArticleRepo(ApplicationDbContext context) : IArticleRepo
    {
         public void Delete(int id)
        {
            var article = context.Articles.Find(id);
            if (article != null)
            {
                context.Articles.Remove(article);
            }
        }
        public List<Article> GetByDoctorId(string doctorId)
        {
            return context.Articles.Where(a => a.DoctorId == doctorId).ToList();
        }

        public List<Article> GetAll()
        {
            return context.Articles.Include(a => a.Doctor).ToList();
        }

        public Article GetById(int id)
        {
            return context.Articles.Include(a => a.Doctor)
                .FirstOrDefault(a => a.Id == id);
        }

        public List<Article> GetPage(int page)
        {
            int pageSize = 5;

            return context.Articles.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetTotalPages(int pageSize)
        {
            return (int)Math.Ceiling((double)context.Articles.Count() / pageSize);
        }

        public void Insert(Article entity)
        {
            context.Articles.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<Article>Search(string searchString, string searchProperty)
        {
            switch (searchProperty)
            {
                case "Title":
                    return context.Articles.Where(a => a.Title.Contains(searchString)).ToList();
                case "Doctor":
                    return context.Articles.Where(a => a.Doctor.FullName.Contains(searchString)).ToList();

                default:
                    return context.Articles.Where(a => a.Content.Contains(searchString)).ToList();
            }
        }

        public void Update(Article entity)
        {
            context.Articles.Update(entity);
        }
    }
}
