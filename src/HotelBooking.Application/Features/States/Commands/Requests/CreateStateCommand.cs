namespace HotelBooking.Application.Features.States.Commands.Requests
{
    public class CreateStateCommand
    {
        public string StateName { get; set; } = default!;
        public int CountryID { get; set; }
    }
}
