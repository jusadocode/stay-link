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
        public DbSet<Feature> Feature { get; set; }
        public DbSet<RoomFeature> RoomFeature { get; set; }
        public DbSet<Booking> Bookings { get; set; }
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

            modelBuilder.Entity<Room>()
            .Property(f => f.RoomType)
            .HasConversion<string>();


            modelBuilder.Entity<RoomFeature>()
            .Property(f => f.Condition)
            .HasConversion<string>();

        }

    }

}
