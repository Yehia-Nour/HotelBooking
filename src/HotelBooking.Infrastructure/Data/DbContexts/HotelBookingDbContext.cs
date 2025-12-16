using HotelBooking.Domain.Entities.Geography;
using HotelBooking.Domain.Entities.Guests;
using HotelBooking.Domain.Entities.Payments;
using HotelBooking.Domain.Entities.Reservations;
using HotelBooking.Domain.Entities.Rooms;
using HotelBooking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        public DbSet<PaymentBatch> PaymentBatches { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<RefundMethod> RefundMethods { get; set; }
        public DbSet<Cancellation> Cancellations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationGuest> ReservationGuests { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
