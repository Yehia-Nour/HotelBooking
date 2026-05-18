namespace HotelBooking.Application.Features.Amenities.Commands.Requests
{
    public record UpdateAmenityWithUserCommand(UpdateAmenityCommand Command, string UserEmail) : IRequest<Result>;
}
