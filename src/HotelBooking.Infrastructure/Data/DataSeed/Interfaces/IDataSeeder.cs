using HotelBooking.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Data.DataSeed.Interfaces
{
    public interface IDataSeeder
    {
        Task SeedAsync<T>(DbSet<T> dbSet, List<T> data) where T : BaseEntity;
    }
}
