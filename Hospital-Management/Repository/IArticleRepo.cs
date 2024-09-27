using Hospital_Management.Models;

namespace Hospital_Management.Repository
{
    public interface IArticleRepo: IRepository<Article, int>
    {
        List<Article> GetByDoctorId(string doctorId);

    }
}
