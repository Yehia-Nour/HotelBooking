using HotelBooking.Domain.Entities.Geography;
using HotelBooking.Domain.Entities.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Entities.Guests
{
    public class Guest
    {
        public int GuestID { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public AgeGroup AgeGroup { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; } = default!;
        public DateTime ModifiedDate { get; set; }

        public string UserID { get; set; } = default!;

        public int CountryID { get; set; }
        public Country Country { get; set; } = default!;

        public int StateID { get; set; }
        public State State { get; set; } = default!;

        public ICollection<ReservationGuest> ReservationGuests { get; set; } = default!;
    }
}
