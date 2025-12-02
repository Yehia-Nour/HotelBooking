using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Entities.Reservations
{
    public class Cancellation
    {
        public int CancellationID { get; set; }

        public DateTime CancellationDate { get; set; }
        public string Reason { get; set; } = default!;
        public decimal CancellationFee { get; set; }
        public CancellationStatus CancellationStatus { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; } = default!;
        public DateTime ModifiedDate { get; set; }

        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; } = default!;
    }
}
