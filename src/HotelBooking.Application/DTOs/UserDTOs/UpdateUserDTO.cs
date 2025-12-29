namespace HotelBooking.Application.DTOs.UserDTOs
{
    public class UpdateUserDTO
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
