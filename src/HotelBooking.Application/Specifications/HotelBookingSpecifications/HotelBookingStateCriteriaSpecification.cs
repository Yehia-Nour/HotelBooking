namespace HotelBooking.Application.Specifications.GeographySpecifications
{
    public class HotelBookingStateCriteriaSpecification : ICriteriaSpecification<State>
    {
        public Expression<Func<State, bool>> Criteria { get; }

        private HotelBookingStateCriteriaSpecification(Expression<Func<State, bool>> criteria)
            => Criteria = criteria;

        public static HotelBookingStateCriteriaSpecification ActiveByIds(IEnumerable<int> stateIds)
        {
            var ids = stateIds.Distinct().ToList();

            return new(s =>
                ids.Contains(s.Id) &&
                s.IsActive
            );
        }
    }
}
