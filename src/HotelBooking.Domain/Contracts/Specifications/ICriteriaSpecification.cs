using HotelBooking.Domain.Entities.Common;
using HotelBooking.Domain.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Contracts.Specifications
{
    public interface ICriteriaSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : BaseEntity
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
    }
}
