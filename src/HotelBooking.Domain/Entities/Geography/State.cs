namespace HotelBooking.Domain.Entities.Geography
{
    public class State
    {
        public int StateID { get; set; }
        public string StateName { get; set; } = default!;
        public bool IsActive { get; set; }

        public int CountryID { get; set; }
        public Country Country { get; set; } = default!;
    }
}