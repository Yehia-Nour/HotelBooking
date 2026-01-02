using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Commands.Requests
{
    public record DeleteRoomAmenityCommand(int RoomTypeId, int AmenityId) : IRequest<Result>;
}
