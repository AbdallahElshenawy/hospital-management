using Hospital_Management.ViewModels;

namespace Hospital_Management.Services
{
    public interface IArticleServices
    {
        public Task<PaginationViewModel<ArticleViewModel>> GetArticles(int page, int pageSize);
        public Task<PaginationViewModel<ArticleViewModel>> Search(int page, int pageSize, string searchString, string? searchProperty = null);
        public Task<ArticleViewModel> GetById(int id);
    }
}
