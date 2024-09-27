using Hospital_Management.Models;
using Hospital_Management.ViewModels;

namespace Hospital_Management.Services
{
    public interface IHomeServices
    {
        public Task<HomeViewModel> GetHomeModel();
    }
}
