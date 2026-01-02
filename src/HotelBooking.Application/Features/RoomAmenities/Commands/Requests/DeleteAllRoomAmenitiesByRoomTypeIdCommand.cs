using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Commands.Requests
{
    public record class DeleteAllRoomAmenitiesByRoomTypeIdCommand(int RoomTypeId) : IRequest<Result>;
}
