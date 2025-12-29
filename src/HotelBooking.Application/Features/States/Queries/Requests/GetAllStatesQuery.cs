using HotelBooking.Application.DTOs.StateDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.States.Queries.Requests
{
    public record GetAllStatesQuery(StateQueryParams QueryParams) : IRequest<Result<IEnumerable<StateDTO>>>;
}
