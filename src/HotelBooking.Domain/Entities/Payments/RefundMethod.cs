using HotelBooking.Domain.Entities.Common;

namespace HotelBooking.Domain.Entities.Payments
{
    public class RefundMethod : BaseEntity
    {
        public string MethodName { get; set; } = default!;
        public bool IsActive { get; set; } = true;
    }
}
