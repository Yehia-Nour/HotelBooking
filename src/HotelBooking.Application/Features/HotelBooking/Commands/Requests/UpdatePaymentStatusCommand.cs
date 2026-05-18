namespace HotelBooking.Application.Features.HotelBooking.Commands.Requests
{
    public class UpdatePaymentStatusCommandRequest
    {
        public int PaymentId { get; init; }
        public PaymentStatus NewStatus { get; init; }
        public string? FailureReason { get; init; }
    }
}
