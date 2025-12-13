using HotelBooking.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Contracts.Specifications
{
    public interface IPaginateSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : BaseEntity
    {
        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
    }
}
