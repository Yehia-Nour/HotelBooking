using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Application.DTOs.CancellationDTOs
{
    public record CancellationRequestListItemDTO
    {
        public int Id { get; init; }
        public int ReservationID { get; init; }
        public string UserId { get; init; } = default!;
        public string CancellationType { get; init; } = default!;
        public DateTime RequestedOn { get; init; }
        public CancellationStatus CancellationStatus { get; init; }
    }
}
