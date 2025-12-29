using HotelBooking.Application.Interfaces.Repositories;
using HotelBooking.Domain.Entities.Common;

namespace HotelBooking.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
