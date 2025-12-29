using HotelBooking.Domain.Entities.Common;
using HotelBooking.Domain.Entities.Geography;
using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Domain.Entities.Guests
{
    public class Guest : AuditableEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public AgeGroup AgeGroup { get; set; } = default!;
        public string Address { get; set; } = default!;

        public string UserID { get; set; } = default!;

        public int CountryID { get; set; }
        public Country Country { get; set; } = default!;

        public int StateID { get; set; }
        public State State { get; set; } = default!;

        public ICollection<ReservationGuest> ReservationGuests { get; set; } = default!;
    }
}
