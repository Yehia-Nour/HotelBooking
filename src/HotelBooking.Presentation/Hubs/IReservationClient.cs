namespace HotelBooking.Presentation.Hubs
{
    public interface IReservationClient
    {
        Task ReceiveRoomReservedNotification(string message, List<int> reservedRoomIds);
    }
}
