namespace HotelBooking.Application.Features.States.Queries.Requests
{
    public record GetStateByIdQuery(int StateId) : IRequest<Result<StateDTO>>;
}
