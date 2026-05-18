namespace HotelBooking.Application.Features.Cancellations.Queries.Requests
{
    public record CalculateCancellationChargesQuery(CalculateCancellationChargesRequest Request) : IRequest<Result<CalculateCancellationChargesResultDTO>>;
}
