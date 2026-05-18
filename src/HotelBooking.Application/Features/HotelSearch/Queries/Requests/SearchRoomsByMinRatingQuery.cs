namespace HotelBooking.Application.Features.HotelSearch.Queries.Requests
{
    public record SearchRoomsByMinRatingQuery(int MinRating) : IRequest<Result<IEnumerable<RoomSearchDTO>>>;
}
