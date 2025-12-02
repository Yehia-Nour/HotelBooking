using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Entities.Geography
{
    public class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; } = default!;
        public string CountryCode { get; set; } = default!;
        public bool IsActive { get; set; }

        public ICollection<State> States { get; set; } = default!;
    }
}
