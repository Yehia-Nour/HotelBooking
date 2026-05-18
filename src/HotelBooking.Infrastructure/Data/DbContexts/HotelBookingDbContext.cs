namespace HotelBooking.Infrastructure.Data.DbContexts
{
    public class HotelBookingDbContext : IdentityDbContext<ApplicationUser>
    {
        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<RefundMethod> RefundMethods { get; set; }
        public DbSet<CancellationRequest> CancellationRequests { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationGuest> ReservationGuests { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ReservationRoom> ReservationRooms { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<CancellationPolicy> CancellationPolicies { get; set; }
        public DbSet<CancellationDetail> CancellationDetails { get; set; }
        public DbSet<CancellationCharge> CancellationCharges { get; set; }
    }
}
