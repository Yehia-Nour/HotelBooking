namespace HotelBooking.Application.Features.HotelSearch.Queries.Requests
{
    public class RoomSearchFilter
    {
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public string? RoomTypeName { get; init; }
        public string? AmenityName { get; init; }
        public string? ViewType { get; init; }
    }
}
