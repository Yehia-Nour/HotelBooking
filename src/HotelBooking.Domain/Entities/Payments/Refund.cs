using HotelBooking.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Entities.Payments
{
    public class Refund : BaseEntity
    {
        public decimal RefundAmount { get; set; }
        public string RefundReason { get; set; } = default!;
        public string RefundStatus { get; set; } = default!;
        public DateTime RefundDate { get; set; }

        public string ProcessedByUserID { get; set; } = default!;

        public int RefundMethodID { get; set; }
        public RefundMethod RefundMethod { get; set; } = default!;

        public int PaymentID { get; set; }
        public Payment Payment { get; set; } = default!;
    }
}
