namespace HotelBooking.Application.Features.States.Commands.Requests
{
    public record ToggleStateActiveCommand(int StateId) : IRequest<Result>;
}
