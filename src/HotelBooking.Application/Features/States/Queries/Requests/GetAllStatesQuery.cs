namespace HotelBooking.Application.Features.States.Queries.Requests
{
    public record GetAllStatesQuery(StateQueryParams QueryParams) : IRequest<Result<IEnumerable<StateDTO>>>;
}
