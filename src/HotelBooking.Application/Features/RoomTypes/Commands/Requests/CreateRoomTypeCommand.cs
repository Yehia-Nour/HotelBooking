namespace HotelBooking.Application.Features.RoomTypes.Commands.Requests
{
    public class CreateRoomTypeCommand
    {
        public string TypeName { get; set; } = default!;
        public string AccessibilityFeatures { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

}
