using HotelBooking.Domain.Entities.Common;

namespace HotelBooking.Domain.Entities.Payments
{
    public class PaymentBatch : BaseEntity
    {
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = default!;

        public string UserID { get; set; } = default!;

        public ICollection<Payment> Payments { get; set; } = default!;
    }
}
