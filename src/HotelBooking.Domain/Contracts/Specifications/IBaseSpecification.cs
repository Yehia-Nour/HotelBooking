using HotelBooking.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Contracts.Specifications
{
    public interface IBaseSpecification<TEntity> where TEntity : BaseEntity { }
}
