using HotelBooking.Presentation.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HotelBooking.Presentation.Services
{
    public class ReservationNotificationService : IReservationNotificationService
    {
        private readonly IHubContext<ReservationHub, IReservationClient> _hubContext;

        public ReservationNotificationService(IHubContext<ReservationHub, IReservationClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyRoomReservedAsync(int reservationId, List<int> roomIds, string message, CancellationToken cancellationToken = default)
           => await _hubContext.Clients.All.ReceiveRoomReservedNotification(message, roomIds);
    }
}
