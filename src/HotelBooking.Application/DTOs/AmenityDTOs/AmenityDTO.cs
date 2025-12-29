namespace HotelBooking.Application.DTOs.AmenityDTOs
{
    public record AmenityDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public bool IsActive { get; init; }
    }
}
