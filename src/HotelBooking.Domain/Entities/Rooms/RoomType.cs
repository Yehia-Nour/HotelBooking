using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Entities.Rooms
{
    public class RoomType
    {
        public int RoomTypeID { get; set; }
        public string TypeName { get; set; } = default!;
        public string AccessibilityFeatures { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; } = default!;
        public DateTime ModifiedDate { get; set; }

        public ICollection<Room> Rooms { get; set; } = default!;
        public ICollection<RoomAmenity> RoomAmenities { get; set; } = default!;
    }
}
