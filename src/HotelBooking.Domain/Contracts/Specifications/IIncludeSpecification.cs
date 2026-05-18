namespace HotelBooking.Domain.Contracts.Specifications
{
    public interface IIncludeSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : BaseEntity
    {
        ICollection<Expression<Func<TEntity, object>>> Includes { get; }
    }
}
