using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Contracts
{
    public interface IDataInitializer
    {
        Task InitializeAsync();
    }
}
