using Hospital_Management.Data;
using Hospital_Management.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Services
{
    public class ArticleServices(ApplicationDbContext context): IArticleServices
    {
        public async Task<PaginationViewModel<ArticleViewModel>> GetArticles(int page, int pageSize)
        {
            var articles = await context.Articles
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(a => a.Doctor)
                .Select(a => new ArticleViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    DoctorName = a.Doctor.FullName,
                    DoctorId = a.Doctor.Id
                })
                .ToListAsync();

            var count = await context.Articles.CountAsync();

            return new PaginationViewModel<ArticleViewModel>
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Items = articles
            };
        }

        public async Task<PaginationViewModel<ArticleViewModel>> Search(int page, int pageSize, string searchString, string? searchProperty = null)
        {
            var query = context.Articles
                .Include(a => a.Doctor)
                .Where(a => a.Content.Contains(searchString) || a.Title.Contains(searchString) || a.Doctor.FirstName.Contains(searchString) || a.Doctor.LastName.Contains(searchString));

            var articles = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new ArticleViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    DoctorName = a.Doctor.FullName,
                    DoctorId = a.Doctor.Id
                })
                .ToListAsync();

            var count = await query.CountAsync();

            return new PaginationViewModel<ArticleViewModel>
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Items = articles
            };
        }

        public async Task<ArticleViewModel> GetById(int id)
        {
            var article = await context.Articles
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);

            return new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                DoctorName = article.Doctor.FullName,
                DoctorId = article.Doctor.Id,
                DoctorImage = article.Doctor.Img,
                DateTime = article.DateTime
            };
        }
    }
}
