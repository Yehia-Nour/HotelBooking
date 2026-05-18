namespace HotelBooking.Application.Features.Rooms.Queries.Requests
{
    public record GetAllRoomsQuery(RoomQueryParams QueryParams) : IRequest<Result<PaginatedResultDTO<RoomDTO>>>;
}