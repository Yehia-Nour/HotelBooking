using HotelBooking.Application.DTOs.HotelSearchDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.HotelSearch.Queries.Requests
{
    public record SearchRoomsByViewTypeQuery(string ViewType) : IRequest<Result<IEnumerable<RoomSearchDTO>>>;
}
