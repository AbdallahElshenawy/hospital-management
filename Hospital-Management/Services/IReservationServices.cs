using Hospital_Management.Models;
using Hospital_Management.ViewModels;

namespace Hospital_Management.Services
{
    public interface IReservationServices
    {
        public Task<DoctorWithResPropertiesViewModel> GetDoctorWithResPropertiesAsync(string doctorId);
        public Task<List<ReservationInListViewModel>> GetReservationsInListAsync(string doctorId, DateTime date);
        public Task<Reservation> ReserveAsync(string doctorId, DateTime date, string userName);
        public Task<ReservationEmailViewModel> GetReservationEmailViewModelAsync(int reservationId);
    }
}
