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
    internal class RoomTypeByNameSpecification : ICriteriaSpecification<RoomType>
    {
        public Expression<Func<RoomType, bool>> Criteria { get; }
        private RoomTypeByNameSpecification(Expression<Func<RoomType, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static RoomTypeByNameSpecification ForName(string name)
        {
            return new RoomTypeByNameSpecification(r => r.TypeName == name);
        }
    }
}
