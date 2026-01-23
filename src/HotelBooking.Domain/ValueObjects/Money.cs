namespace HotelBooking.Domain.ValueObjects
{
    public readonly record struct Money(decimal Value)
    {
        public static Money Zero => new(0m);

        public static Money operator +(Money a, Money b)
            => new(a.Value + b.Value);

        public static Money operator *(Money a, int multiplier)
            => new(a.Value * multiplier);

        public static Money operator *(Money a, decimal multiplier)
            => new(a.Value * multiplier);

        public override string ToString() => Value.ToString("0.00");
    }
}
