using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Entities.Payments
{
    public class RefundMethod
    {
        public int MethodID { get; set; }
        public string MethodName { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
