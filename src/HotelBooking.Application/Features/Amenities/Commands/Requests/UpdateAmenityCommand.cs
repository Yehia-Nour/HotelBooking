using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.Amenities.Commands.Requests
{
    public class UpdateAmenityCommand
    {
        public int AmenityId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
