namespace HotelBooking.Application.Features.Amenities.Commands.Requests
{
    public record ToggleAmenityActiveCommand(int AmenityId) : IRequest<Result>;
}
