using HotelBooking.Domain.Entities.Guests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Entities.Reservations
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = default!;
        public DateTime FeedbackDate { get; set; }

        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; } = default!;

        public int GuestID { get; set; }
        public Guest Guest { get; set; } = default!;
    }
}
