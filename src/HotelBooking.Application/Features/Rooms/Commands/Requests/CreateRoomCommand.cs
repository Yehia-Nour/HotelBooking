using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.Rooms.Commands.Requests
{
    public class CreateRoomCommand
    {
        public string RoomNumber { get; set; } = default!;
        public int RoomTypeID { get; set; }
        public decimal Price { get; set; }
        public string BedType { get; set; } = default!;
        public string ViewType { get; set; } = default!;
        public RoomStatus Status { get; set; }
        public bool IsActive { get; set; }
    }
}
