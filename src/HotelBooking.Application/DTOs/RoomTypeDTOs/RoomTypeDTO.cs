using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.DTOs.RoomTypeDTOs
{
    public class RoomTypeDTO
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = default!;
        public string AccessibilityFeatures { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
