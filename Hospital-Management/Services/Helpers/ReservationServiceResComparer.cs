using Hospital_Management.ViewModels;

namespace Hospital_Management.Services.Helpers
{
    public class ReservationServiceResComparer : IEqualityComparer<ReservationInListViewModel>
    {
        public bool Equals(ReservationInListViewModel x, ReservationInListViewModel y)
        {
            if (x == null || y == null)
                return false;

            return x.Date == y.Date;
        }

        public int GetHashCode(ReservationInListViewModel obj)
        {
            return obj.Date.GetHashCode();
        }
    }
}
