using Hospital_Management.Models;

namespace Hospital_Management.Repository
{
    public interface IRateRepo: IRepository<Rate, int>
    {
        List<Rate> GetByDoctorId(string doctorId);

    }
}
