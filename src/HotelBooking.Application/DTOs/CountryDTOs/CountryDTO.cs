namespace HotelBooking.Application.DTOs.CountryDTOs
{
    public record CountryDTO
    {
        public int Id { get; init; }
        public string CountryName { get; init; } = default!;
        public string CountryCode { get; init; } = default!;
        public bool IsActive { get; init; }
    }

}
