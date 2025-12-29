namespace HotelBooking.Application.Features.Amenities.Commands.Requests
{
    public class CreateAmenitiesCommand
    {
        public List<CreateAmenityCommand> Amenities { get; set; } = new();
    }

}
