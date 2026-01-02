using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Commands.Requests
{
    public class CreateRoomAmenitiesToRoomTypeCommand : IRequest<Result>
    {
        public int RoomTypeId { get; set; }
        public List<int> AmenityIds { get; set; } = [];
    }
}