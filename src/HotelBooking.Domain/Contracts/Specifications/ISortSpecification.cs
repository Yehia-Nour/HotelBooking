using HotelBooking.Domain.Entities.Common;
using System.Linq.Expressions;

namespace HotelBooking.Domain.Contracts.Specifications
{
    public interface ISortSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : BaseEntity
    {
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }
    }
}
