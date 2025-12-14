using HotelBooking.Infrastructure.Data.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Data.Identity.Security
{
    public interface IJwtService
    {
        Task<string> CreateTokenAsync(ApplicationUser user);
    }
}
