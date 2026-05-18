namespace HotelBooking.Domain.Contracts.Specifications
{
    public interface ICriteriaSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : BaseEntity
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
    }
}
