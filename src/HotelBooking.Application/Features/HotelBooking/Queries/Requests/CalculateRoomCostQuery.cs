namespace HotelBooking.Application.Features.HotelBooking.Queries.Requests
{
    public record CalculateRoomCostQuery(CalculateRoomCostRequest Request) : IRequest<Result<RoomCostResultDTO>>;
}
