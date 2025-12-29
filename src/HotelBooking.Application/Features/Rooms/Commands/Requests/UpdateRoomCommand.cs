namespace HotelBooking.Application.Features.Rooms.Commands.Requests
{
    public class UpdateRoomCommand
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = default!;
        public int RoomTypeID { get; set; }
        public decimal Price { get; set; }
        public string BedType { get; set; } = default!;
        public string ViewType { get; set; } = default!;
        public RoomStatus Status { get; set; }
    }
}
