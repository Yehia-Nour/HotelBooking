using HotelBooking.Application.Features.HotelSearch.Queries.Requests;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Reservations;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.HotelSearchSpecifications
{
    internal class HotelSearchCriteriaSpecification : ICriteriaSpecification<Room>
    {
        public Expression<Func<Room, bool>> Criteria { get; }
        private HotelSearchCriteriaSpecification(Expression<Func<Room, bool>> criteria)
             => Criteria = criteria;

        public static HotelSearchCriteriaSpecification Available()
            => new(r =>
                r.IsActive
                && r.Status == BookingStatus.Available
                && !r.Reservations.Any(res => res.Status != ReservationStatus.Cancelled)
             );

        public static HotelSearchCriteriaSpecification ByAvailableWithinDates(DateTime checkInDate, DateTime checkOutDate)
            => new(r =>
                r.IsActive
                && r.Status == BookingStatus.Available
                && !r.Reservations.Any(res => res.Status != ReservationStatus.Cancelled
                && res.CheckInDate <= checkOutDate
                && res.CheckOutDate >= checkInDate)
            );

        public static HotelSearchCriteriaSpecification ByAvailableWithinPriceRange(decimal minPrice, decimal maxPrice)
            => new(r =>
                r.IsActive
                && r.Status == BookingStatus.Available
                && r.Price >= minPrice
                && r.Price <= maxPrice
                && !r.Reservations.Any(res => res.Status != ReservationStatus.Cancelled)
            );

        public static HotelSearchCriteriaSpecification ByRoomTypeName(string roomTypeName)
            => new(r =>
                r.IsActive
                && r.Status == BookingStatus.Available
                && r.RoomType.TypeName.Contains(roomTypeName)
                && !r.Reservations.Any(res => res.Status != ReservationStatus.Cancelled)
            );

        public static HotelSearchCriteriaSpecification ByViewType(string viewType)
            => new(r =>
                r.IsActive
                && r.Status == BookingStatus.Available
                && r.ViewType.Contains(viewType)
                && !r.Reservations.Any(res => res.Status != ReservationStatus.Cancelled)
            );

        public static HotelSearchCriteriaSpecification ByAmenity(string amenityName)
            => new(r =>
                r.IsActive
                && r.Status == BookingStatus.Available
                && r.RoomType.RoomAmenities.Any(ra => ra.Amenity.Name.Contains(amenityName))
                && !r.Reservations.Any(res => res.Status != ReservationStatus.Cancelled)
            );

        public static HotelSearchCriteriaSpecification ByRoomId(int roomId)
            => new(r =>
                r.IsActive
                && r.Id == roomId
                && r.Status == BookingStatus.Available
            );

        public static HotelSearchCriteriaSpecification ByMinAverageRating(int minRating)
            => new(r =>
                r.IsActive
                && r.Status == BookingStatus.Available
                && r.Reservations.Any(res => res.Feedback != null)
                && r.Reservations
                    .Where(res => res.Feedback != null)
                    .Average(res => (double)res.Feedback!.Rating) >= minRating
            );

        public static HotelSearchCriteriaSpecification ByCustomFilter(RoomSearchFilter filter)
            => new(r =>
                r.IsActive
                && r.Status == BookingStatus.Available

                && (!filter.MinPrice.HasValue || r.Price >= filter.MinPrice.Value)
                && (!filter.MaxPrice.HasValue || r.Price <= filter.MaxPrice.Value)

                && (string.IsNullOrWhiteSpace(filter.RoomTypeName)
                    || r.RoomType.TypeName.Contains(filter.RoomTypeName))

                && (string.IsNullOrWhiteSpace(filter.AmenityName)
                    || r.RoomType.RoomAmenities
                        .Any(ra => ra.Amenity.Name.Contains(filter.AmenityName)))

                && (string.IsNullOrWhiteSpace(filter.ViewType)
                    || r.ViewType == filter.ViewType)

                && !r.Reservations.Any(res => res.Status != ReservationStatus.Cancelled)
            );
    }
}