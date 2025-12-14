using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Data.Identity
{
    public interface IJwtService
    {
        Task<string> CreateTokenAsync(IdentityUser user);
    }
}
