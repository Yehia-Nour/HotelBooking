using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.DTOs.RoomDTOs
{
    public class RoomQueryParams
    {
        public int? RoomTypeId {  get; set; }
        public string? SearchByRoomNumber { get; set; }
        public RoomStatus? Status { get; set; }
        public RoomSortingOption sortingOption { get; set; }

        private int _pageIndex = 1;
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = (value <= 0 ? 1 : value);
            }
        }

        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        private int _pageSize = DefaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value <= 0)
                    _pageSize = DefaultPageSize;
                else if (value > MaxPageSize)
                    _pageSize = MaxPageSize;
                else
                    _pageSize = value;
            }
        }
    }
}
