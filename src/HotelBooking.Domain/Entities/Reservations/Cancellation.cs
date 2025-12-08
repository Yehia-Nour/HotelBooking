using HotelBooking.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Entities.Reservations
{
    public class Cancellation : AuditableEntity
    {
        public DateTime CancellationDate { get; set; }
        public string Reason { get; set; } = default!;
        public decimal CancellationFee { get; set; }
        public CancellationStatus CancellationStatus { get; set; } = default!;

        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; } = default!;
    }
}
