namespace HotelBooking.Application.DTOs.RoomDTOs
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
