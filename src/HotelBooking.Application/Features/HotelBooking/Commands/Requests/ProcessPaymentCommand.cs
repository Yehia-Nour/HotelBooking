namespace HotelBooking.Application.Features.HotelBooking.Commands.Requests
{
    public class ProcessPaymentCommand
    {
        public int ReservationId { get; init; }
        public decimal TotalAmount { get; init; }
        public string PaymentMethod { get; init; } = default!;
    }
}
