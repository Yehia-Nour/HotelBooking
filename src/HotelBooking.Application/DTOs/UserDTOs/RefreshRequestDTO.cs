using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.DTOs.UserDTOs
{
    public class RefreshRequestDTO
    {
        public string UserId { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
