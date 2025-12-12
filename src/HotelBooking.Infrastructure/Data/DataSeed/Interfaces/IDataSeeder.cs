using HotelBooking.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Data.DataSeed.Interfaces
{
    public interface IDataSeeder
    {
        Task SeedAsync<T>(DbSet<T> dbSet, List<T> data) where T : BaseEntity;
    }
}
