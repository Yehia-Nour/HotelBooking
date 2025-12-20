using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.DTOs.UserDTOs
{
    public class ChangePasswordDTO
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
