namespace HotelBooking.Application.Features.RoomAmenities.Commands.Requests
{
    public class CreateRoomAmenityCommand : IRequest<Result>
    {
        public int RoomTypeId { get; set; }
        public int AmenityId { get; set; }
    }
}
