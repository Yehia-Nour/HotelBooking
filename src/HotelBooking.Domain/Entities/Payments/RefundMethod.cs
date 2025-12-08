using HotelBooking.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Entities.Payments
{
    public class RefundMethod : BaseEntity
    {
        public string MethodName { get; set; } = default!;
        public bool IsActive { get; set; } = true;
    }
}
