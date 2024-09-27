using Hospital_Management.ViewModels;

namespace Hospital_Management.Services
{
    public interface IDoctorServices
    {
        public Task<PaginationViewModel<DoctorViewModel>> GetDoctors(int page, int pageSize);
        public Task<PaginationViewModel<DoctorViewModel>> Search(int page, int pageSize, string searchString, string? searchProperty = null);
        public Task<DoctorViewModel> GetById(string id);
    }
}
