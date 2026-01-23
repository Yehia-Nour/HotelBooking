using HotelBooking.Application.DTOs.HotelBookingDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.HotelBooking.Queries.Requests
{
    public record CalculateRoomCostQuery(CalculateRoomCostRequest Request) : IRequest<Result<RoomCostResultDTO>>;
}
