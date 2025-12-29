using HotelBooking.Domain.Entities.Common;

namespace HotelBooking.Domain.Entities.Geography
{
    public class Country : BaseEntity
    {
        public string CountryName { get; set; } = default!;
        public string CountryCode { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        public ICollection<State> States { get; set; } = default!;
    }
}
