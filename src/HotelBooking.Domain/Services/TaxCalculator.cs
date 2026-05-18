namespace HotelBooking.Domain.Services
{
    public static class TaxCalculator
    {
        private const decimal GstRate = 0.18m;

        public static Money CalculateGst(Money amount)
            => amount * GstRate;

        public static Money CalculateTotal(Money amount)
            => amount + CalculateGst(amount);
    }
}
