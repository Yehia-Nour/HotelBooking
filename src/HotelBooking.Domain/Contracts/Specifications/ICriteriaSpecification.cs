using HotelBooking.Domain.Entities.Common;
using System.Linq.Expressions;

namespace HotelBooking.Domain.Contracts.Specifications
{
    public interface ICriteriaSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : BaseEntity
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
    }
}
