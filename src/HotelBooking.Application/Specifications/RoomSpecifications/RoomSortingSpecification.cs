using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Specifications.RoomSpecifications
{
    internal class RoomSortingSpecification : ISortSpecification<Room>
    {
        public Expression<Func<Room, object>> OrderBy { get; private set; }
        public Expression<Func<Room, object>> OrderByDescending { get; private set; }

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
