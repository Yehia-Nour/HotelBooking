namespace HotelBooking.Application.DTOs.StateDTOs
{
    public record StateDTO
    {
        public int Id { get; init; }
        public string StateName { get; init; } = default!;
        public bool IsActive { get; init; }
    }
}
