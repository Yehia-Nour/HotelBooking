namespace HotelBooking.Application.Features.States.Commands.Requests
{
    public record UpdateStateWithUserCommand(UpdateStateCommand Command, string UserEmail) : IRequest<Result>;
}
