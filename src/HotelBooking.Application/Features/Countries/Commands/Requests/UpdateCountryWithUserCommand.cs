namespace HotelBooking.Application.Features.Countries.Commands.Requests
{
    public record UpdateCountryWithUserCommand(UpdateCountryCommand Command, string UserEmail) : IRequest<Result>;
}
