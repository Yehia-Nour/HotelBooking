using HotelBooking.Application.DTOs.CancellationDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Cancellations.Queries.Requests
{
    public record CalculateCancellationChargesQuery(CalculateCancellationChargesRequest Request) : IRequest<Result<CalculateCancellationChargesResultDTO>>;
}
