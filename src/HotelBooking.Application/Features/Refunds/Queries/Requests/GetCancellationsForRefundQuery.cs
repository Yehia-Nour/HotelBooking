using HotelBooking.Application.DTOs.CancellationDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Refunds.Queries.Requests
{
    public record GetCancellationsForRefundQuery() : IRequest<Result<IEnumerable<CancellationForRefundDTO>>>;
}
