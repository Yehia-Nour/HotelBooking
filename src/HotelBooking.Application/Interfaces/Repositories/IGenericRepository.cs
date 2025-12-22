using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ICollection<IBaseSpecification<TEntity>> specifications);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetAsync(ICollection<IBaseSpecification<TEntity>> specifications);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
