using System.Runtime.Serialization;

namespace HotelBooking.Domain.Entities.Reservations
{
    public enum ReservationStatus
    {
        Reserved,
        CheckedIn,
        CheckedOut,
        Cancelled
    }
}