using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomSpecifications
{
    internal class RoomSortingSpecification : ISortSpecification<Room>
    {
        public Expression<Func<Room, object>> OrderBy { get; private set; } = null!;
        public Expression<Func<Room, object>> OrderByDescending { get; private set; } = null!;

        private RoomSortingSpecification() { }

        public static RoomSortingSpecification ByOption(RoomSortingOption option)
        {
            var spec = new RoomSortingSpecification();

            switch (option)
            {
                case RoomSortingOption.RoomNumberAsc:
                    spec.OrderBy = r => r.RoomNumber;
                    break;
                case RoomSortingOption.RoomNumberDesc:
                    spec.OrderByDescending = r => r.RoomNumber;
                    break;
                case RoomSortingOption.PriceAsc:
                    spec.OrderBy = r => r.Price;
                    break;
                case RoomSortingOption.PriceDesc:
                    spec.OrderByDescending = r => r.Price;
                    break;
                default:
                    spec.OrderBy = r => r.Id;
                    break;
            }

            return spec;
        }
    }
}
