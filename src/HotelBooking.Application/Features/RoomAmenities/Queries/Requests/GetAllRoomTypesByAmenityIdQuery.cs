namespace HotelBooking.Application.Features.RoomAmenities.Queries.Requests
{
    public record GetAllRoomTypesByAmenityIdQuery(int AmenityId) : IRequest<Result<IEnumerable<RoomTypeDTO>>>;
}
