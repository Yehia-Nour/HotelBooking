using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity>(
            IQueryable<TEntity> query,
            ICollection<IBaseSpecification<TEntity>> specifications) where TEntity : BaseEntity
        {
            foreach (var spec in specifications)
            {
                switch (spec)
                {
                    case ICriteriaSpecification<TEntity> criteriaSpec:
                        query = ApplyCriteria(query, criteriaSpec);
                        break;

                    case IIncludeSpecification<TEntity> includeSpec:
                        query = ApplyIncludes(query, includeSpec);
                        break;

                    case ISortSpecification<TEntity> sortSpec:
                        query = ApplySorting(query, sortSpec);
                        break;

                    case IPaginateSpecification<TEntity> pageSpec:
                        query = ApplyPagination(query, pageSpec);
                        break;
                }
            }

            return query;
        }

        private static IQueryable<TEntity> ApplyCriteria<TEntity>(IQueryable<TEntity> query, ICriteriaSpecification<TEntity> spec) where TEntity : BaseEntity
            => spec.Criteria is not null ? query.Where(spec.Criteria) : query;


        private static IQueryable<TEntity> ApplyIncludes<TEntity>(IQueryable<TEntity> query, IIncludeSpecification<TEntity> spec) where TEntity : BaseEntity
            => spec.Includes.Any() ? spec.Includes.Aggregate(query, (current, include) => current.Include(include)) : query;

        private static IQueryable<TEntity> ApplySorting<TEntity>(IQueryable<TEntity> query, ISortSpecification<TEntity> spec) where TEntity : BaseEntity
        {
            if (spec.OrderBy is not null) return query.OrderBy(spec.OrderBy);
            if (spec.OrderByDescending is not null) return query.OrderByDescending(spec.OrderByDescending);
            return query;
        }

        private static IQueryable<TEntity> ApplyPagination<TEntity>(IQueryable<TEntity> query, IPaginateSpecification<TEntity> spec) where TEntity : BaseEntity
            => spec.IsPaginated ? query.Skip(spec.Skip).Take(spec.Take) : query;
    }

}
