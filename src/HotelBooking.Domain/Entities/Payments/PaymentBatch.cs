using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Entities.Payments
{
    public class PaymentBatch
    {
        public int PaymentBatchID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = default!;

        public string UserID { get; set; } = default!;

        public ICollection<Payment> Payments { get; set; } = default!;
    }
}
