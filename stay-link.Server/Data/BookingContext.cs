using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Auth;
using stay_link.Server.Auth.Model;
using stay_link.Server.Models;

namespace stay_link.Server.Data
{
    public class BookingContext : IdentityDbContext<BookingUser>
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<RoomUsage> RoomUsages { get; set; }
        public DbSet<RoomFeature> RoomFeatures { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToLower());
                }
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoomUsage>()
            .Property(r => r.CleaningState)
            .HasConversion<string>();

            modelBuilder.Entity<Room>()
            .Property(r => r.RoomType)
            .HasConversion<string>();


            modelBuilder.Entity<Booking>()
            .Property(r => r.Status)
            .HasConversion<string>();

            modelBuilder.Entity<Room>()
        .HasMany(r => r.Features)
        .WithMany(rf => rf.Rooms)
        .UsingEntity<Dictionary<string, object>>(
            "RoomRoomFeature", // Name of the join table
            j => j.HasOne<RoomFeature>().WithMany().HasForeignKey("RoomFeatureID"),
            j => j.HasOne<Room>().WithMany().HasForeignKey("RoomID")
        );

            // Configure many-to-many relationship between Booking and RoomFeature
            modelBuilder.Entity<Booking>()
                .HasMany(b => b.FeaturePreferences)
                .WithMany(rf => rf.Bookings)
                .UsingEntity<Dictionary<string, object>>(
                    "BookingRoomFeature", // Name of the join table
                    j => j.HasOne<RoomFeature>().WithMany().HasForeignKey("RoomFeatureID"),
                    j => j.HasOne<Booking>().WithMany().HasForeignKey("BookingID")
                );

        }

    }

}
