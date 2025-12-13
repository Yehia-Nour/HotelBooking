using HotelBooking.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Contracts.Specifications
{
    public interface IIncludeSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : BaseEntity
    {
        ICollection<Expression<Func<TEntity, object>>> Includes { get; }
    }
}
