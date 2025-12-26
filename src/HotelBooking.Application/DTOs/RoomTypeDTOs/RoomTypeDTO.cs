using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.DTOs.RoomTypeDTOs
{
    public record RoomTypeDTO
    {
        public int Id { get; init; }
        public string TypeName { get; init; } = default!;
        public string AccessibilityFeatures { get; init; } = default!;
        public string Description { get; init; } = default!;
        public bool IsActive { get; init; }
    }

}
