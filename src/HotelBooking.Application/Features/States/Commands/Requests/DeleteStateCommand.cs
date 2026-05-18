namespace HotelBooking.Application.Features.States.Commands.Requests
{
    public record DeleteStateCommand(int StateId) : IRequest<Result>;
}
