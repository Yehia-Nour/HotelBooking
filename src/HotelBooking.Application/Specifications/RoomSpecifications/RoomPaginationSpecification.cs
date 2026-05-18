namespace HotelBooking.Application.Specifications.RoomSpecifications
{
    internal class RoomPaginationSpecification : IPaginateSpecification<Room>
    {
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; private set; }

        private RoomPaginationSpecification() { }

        public static RoomPaginationSpecification ForQuery(RoomQueryParams queryParams)
        {
            return new()
            {
                Take = queryParams.PageSize,
                Skip = (queryParams.PageIndex - 1) * queryParams.PageSize,
                IsPaginated = true
            };
        }
    }
}
