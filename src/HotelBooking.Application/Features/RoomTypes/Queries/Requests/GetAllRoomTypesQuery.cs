namespace HotelBooking.Application.Features.RoomTypes.Queries.Requests
{
    public record GetAllRoomTypesQuery(bool? IsActive) : IRequest<Result<IEnumerable<RoomTypeDTO>>>;
}
