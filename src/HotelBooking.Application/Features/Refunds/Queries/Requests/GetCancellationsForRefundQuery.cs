namespace HotelBooking.Application.Features.Refunds.Queries.Requests
{
    public record GetCancellationsForRefundQuery() : IRequest<Result<IEnumerable<CancellationForRefundDTO>>>;
}
