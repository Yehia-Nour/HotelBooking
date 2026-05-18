namespace HotelBooking.Domain.Entities.Reservations
{
    public class CancellationDetail : BaseEntity
    {
        public int? CancellationRequestId { get; set; }
        public int? ReservationRoomId { get; set; }

        public CancellationRequest? CancellationRequest { get; set; }
        public ReservationRoom? ReservationRoom { get; set; }
    }
}