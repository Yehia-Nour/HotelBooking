namespace HotelBooking.Application.Services.Interfaces
{
    public interface IReservationNotificationService
    {
        Task NotifyRoomReservedAsync(int reservationId, List<int> roomIds, string message, CancellationToken cancellationToken = default);
    }
}
