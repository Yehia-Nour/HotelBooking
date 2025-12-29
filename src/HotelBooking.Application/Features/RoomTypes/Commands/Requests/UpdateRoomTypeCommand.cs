namespace HotelBooking.Application.Features.RoomTypes.Commands.Requests
{
    public class UpdateRoomTypeCommand
    {
        public int RoomTypeId { get; set; }
        public string TypeName { get; set; } = default!;
        public string AccessibilityFeatures { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
