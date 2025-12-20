using HotelBooking.Application.Interfaces.Repositories;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Common;
using HotelBooking.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Implementations.Repositories
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly HotelBookingDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(HotelBookingDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(ICollection<IBaseSpecification<TEntity>> specifications)
                => await SpecificationEvaluator.CreateQuery(_dbSet, specifications).ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<TEntity?> GetByIdAsync(ICollection<IBaseSpecification<TEntity>> specifications)
                => await SpecificationEvaluator.CreateQuery(_dbSet, specifications).FirstOrDefaultAsync();

        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

        public void Update(TEntity entity) => _dbSet.Update(entity);

        public void Delete(TEntity entity) => _dbSet.Remove(entity);
    }
}
