using Hospital_Management.Areas.AdminPortal.ViewModel;
using Hospital_Management.Models;
using Hospital_Management.Repository;

namespace Hospital_Management.Areas.AdminPortal.Repository
{
    public interface IDocRepo: IRepository<Doctor, string>
    {
        public DoctorDetailsVM? BindGetDoctorDetails(string doctorId);
    }
}
