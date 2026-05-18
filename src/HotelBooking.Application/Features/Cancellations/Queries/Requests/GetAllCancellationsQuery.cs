namespace HotelBooking.Application.Features.Cancellations.Queries.Requests
{
    public record GetAllCancellationsQuery(GetAllCancellationsRequest Request) : IRequest<Result<IEnumerable<CancellationRequestListItemDTO>>>;
}
