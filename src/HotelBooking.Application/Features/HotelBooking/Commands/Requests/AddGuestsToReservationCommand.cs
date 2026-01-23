namespace HotelBooking.Application.Features.HotelBooking.Commands.Requests
{
    public class AddGuestsToReservationCommand
    {
        public int ReservationId { get; init; }
        public List<AddGuestToReservationItem> Guests { get; init; } = [];
    }
}