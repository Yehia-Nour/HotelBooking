using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Specifications.RoomTypeSpecifications
{
    public class RoomTypeByActiveSpecification : ICriteriaSpecification<RoomType>
    {
        public Expression<Func<RoomType, bool>> Criteria { get; }

        private RoomTypeByActiveSpecification(Expression<Func<RoomType, bool>> criteria) 
        {
            Criteria = criteria;
        }

        public static RoomTypeByActiveSpecification ForStatus(bool? isActive)
        {
            if (isActive is null)
                return new RoomTypeByActiveSpecification(r => true);

            return new (r => r.IsActive == isActive.Value);
        }
    }
}
