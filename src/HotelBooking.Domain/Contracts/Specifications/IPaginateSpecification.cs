using HotelBooking.Domain.Entities.Common;

namespace HotelBooking.Domain.Contracts.Specifications
{
    public interface IPaginateSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : BaseEntity
    {
        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
    }
}
