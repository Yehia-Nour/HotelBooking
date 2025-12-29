using HotelBooking.Domain.Entities.Common;

namespace HotelBooking.Domain.Entities.Geography
{
    public class State : AuditableEntity
    {
        public string StateName { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        public int CountryID { get; set; }
        public Country Country { get; set; } = default!;
    }
}