using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
