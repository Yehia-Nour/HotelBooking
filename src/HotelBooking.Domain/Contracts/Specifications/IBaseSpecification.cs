using HotelBooking.Domain.Entities.Common;

namespace HotelBooking.Domain.Contracts.Specifications
{
    public interface IBaseSpecification<TEntity> where TEntity : BaseEntity { }
}
