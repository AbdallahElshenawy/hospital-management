using Hospital_Management.Models;
namespace Hospital_Management.Repository
{
    public interface IReservationRepo: IRepository<Reservation, int>
    {
        List<Reservation> GetByDoctorId(string doctorId);
    }
}
