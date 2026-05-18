namespace HotelBooking.Application.Features.RoomAmenities.Queries.Requests
{
    public record GetAllAmenitiesByRoomTypeIdQuery(int RoomTypeId) : IRequest<Result<IEnumerable<AmenityDTO>>>;
}
