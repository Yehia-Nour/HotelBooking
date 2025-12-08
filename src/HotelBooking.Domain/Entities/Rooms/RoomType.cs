using HotelBooking.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Entities.Rooms
{
    public class RoomType : AuditableEntity
    {
        public string TypeName { get; set; } = default!;
        public string AccessibilityFeatures { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        public ICollection<Room> Rooms { get; set; } = default!;
        public ICollection<RoomAmenity> RoomAmenities { get; set; } = default!;
    }
}
