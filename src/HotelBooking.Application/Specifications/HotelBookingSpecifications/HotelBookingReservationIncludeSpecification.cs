using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Reservations;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.HotelBookingSpecifications
{
    public class HotelBookingReservationIncludeSpecification : IIncludeSpecification<Reservation>
    {
        public ICollection<Expression<Func<Reservation, object>>> Includes { get; }

        private HotelBookingReservationIncludeSpecification(ICollection<Expression<Func<Reservation, object>>> includes)
        {
            Includes = includes;
        }

        public static HotelBookingReservationIncludeSpecification ReservationRooms()
            => new(new List<Expression<Func<Reservation, object>>> { r => r.ReservationRooms });
    }
}