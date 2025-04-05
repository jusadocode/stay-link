using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Helpers;
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

            modelBuilder.Entity<BookingUser>(entity => { entity.ToTable(name: "Users"); });
            modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Features)
                .WithMany(rf => rf.Rooms)
                .UsingEntity<Dictionary<string, object>>(
                    "room_room_features", // Name of the join table
                    j => j.HasOne<RoomFeature>().WithMany().HasForeignKey("room_feature_id"),
                    j => j.HasOne<Room>().WithMany().HasForeignKey("room_id")
                );

            // Configure many-to-many relationship between Booking and RoomFeature
            modelBuilder.Entity<Booking>()
                .HasMany(b => b.FeaturePreferences)
                .WithMany(rf => rf.Bookings)
                .UsingEntity<Dictionary<string, object>>(
                    "booking_room_features", // Name of the join table
                    j => j.HasOne<RoomFeature>().WithMany().HasForeignKey("room_feature_id"),
                    j => j.HasOne<Booking>().WithMany().HasForeignKey("booking_id")
                );

            modelBuilder.Entity<Booking>()
               .HasMany(b => b.Rooms)
               .WithMany(r => r.Bookings) 
               .UsingEntity<Dictionary<string, object>>(
                   "booking_rooms",
                   j => j.HasOne<Room>().WithMany().HasForeignKey("room_id"),
                   j => j.HasOne<Booking>().WithMany().HasForeignKey("booking_id")
               );

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Convert table name to snake case
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                // Convert column names to snake case
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                }

                // Convert foreign key names to snake case
                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                // Convert index names to snake case
                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
                }
            }
        }



    }

}
