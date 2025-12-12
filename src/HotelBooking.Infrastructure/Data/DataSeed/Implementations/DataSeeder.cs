using HotelBooking.Domain.Entities.Common;
using HotelBooking.Infrastructure.Data.DataSeed.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Data.DataSeed.Implementations
{
    public class DataSeeder : IDataSeeder
    {
        public async Task SeedAsync<T>(DbSet<T> dbSet, List<T> data) where T : BaseEntity
        {
            if (data is null || !data.Any()) return;

            await dbSet.AddRangeAsync(data);
        }
    }
}
