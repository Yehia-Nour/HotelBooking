namespace HotelBooking.Application.Features.Amenities.Commands.Requests
{
    public class CreateAmenityCommand
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
