using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Data.DataSeed.Interfaces
{
    public interface IDataInitializer
    {
        Task InitializeAsync();
    }
}
