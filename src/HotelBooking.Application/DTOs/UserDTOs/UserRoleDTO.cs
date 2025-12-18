using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.DTOs.UserDTOs
{
    public class UserRoleDTO
    {
        public string UserId { get; set; } = null!;
        public Role Role { get; set; }
    }
}
