using HotelBooking.Domain.Entities.Guests;

namespace HotelBooking.Application.Features.HotelBooking.Commands.Requests
{
    public class AddGuestToReservationItem
    {
        public int RoomId { get; init; }

        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Phone { get; init; } = default!;

        public AgeGroup AgeGroup { get; init; }

        public string Address { get; init; } = default!;
        public int CountryId { get; init; }
        public int StateId { get; init; }
    }
}
