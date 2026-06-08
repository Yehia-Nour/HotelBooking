using Microsoft.AspNetCore.SignalR;

namespace HotelBooking.Presentation.Hubs
{
    public class ReservationHub : Hub<IReservationClient> { }
}
