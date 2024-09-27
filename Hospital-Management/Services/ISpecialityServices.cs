using Hospital_Management.Models;
using Hospital_Management.ViewModels;

namespace Hospital_Management.Services
{
    public interface ISpecialityServices
    {
        public Task<List<SpecialityViewModel>> GetSpecialities();
        public Task<SpecialityWithDoctorsViewModel> GetById(int id);
    }
}
