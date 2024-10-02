using Microsoft.EntityFrameworkCore;
using stay_link.Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace stay_link.Server.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvIDer)
        {
            using (var context = new BookingContext(
                serviceProvIDer.GetRequiredService<DbContextOptions<BookingContext>>()))
            {
                // Ensure the database is created.
                context.Database.EnsureCreated();

                // Check if any hotels exist.
                if (context.Hotel.Any())
                {
                    return; // Database has been seeded
                }

                // Seed hotels
                var hotels = new[]
                {
                    new Hotel { Name = "Grand Hotel", Address = "123 Main St, Springfield", ImageUrl = "https://149345965.v2.pressablecdn.com/wp-content/uploads/img-hotels-IADGV_006-Dusk-Exterior-home.jpg" },
                    new Hotel { Name = "Ocean Breeze Hotel", Address = "456 Ocean Ave, Miami", ImageUrl = "https://images.unsplash.com/photo-1587213811864-46e59f6873b1?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixID=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    new Hotel { Name = "Purple Forest Hotel", Address = "12 Wolf Street, Portland", ImageUrl = "https://images.unsplash.com/photo-1517840901100-8179e982acb7?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.0.3&ixID=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    new Hotel { Name = "Mountain View Inn", Address = "789 Mountain Rd, Denver", ImageUrl = "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.0.3&ixID=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    new Hotel { Name = "City Lights Hotel", Address = "101 City Center Blvd, New York", ImageUrl = "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.0.3&ixID=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    new Hotel { Name = "Sunset Resort", Address = "202 Sunset Dr, Malibu", ImageUrl = "https://images.unsplash.com/photo-1517840901100-8179e982acb7?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.0.3&ixID=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    new Hotel { Name = "Forest Retreat", Address = "303 Forest Ln, Asheville", ImageUrl = "https://images.unsplash.com/photo-1580041065738-e72023775cdc?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixID=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    new Hotel { Name = "RiversIDe Hotel", Address = "404 River St, Chicago", ImageUrl = "https://images.unsplash.com/photo-1517840901100-8179e982acb7?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.0.3&ixID=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    new Hotel { Name = "Desert Oasis", Address = "505 Desert Rd, Phoenix", ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixID=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    new Hotel { Name = "Snowy Peaks Hotel", Address = "606 Snowy Rd, Aspen", ImageUrl = "https://images.unsplash.com/photo-1580041065738-e72023775cdc?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixID=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" }
                };

                context.Hotel.AddRange(hotels);
                context.SaveChanges();

                // Seed rooms and associate with hotels
                var rooms = new List<Room>
                {
                    new Room("Standard room with garden view", RoomType.Standart, hotels[0].ID),
                    new Room("Deluxe room with sea view", RoomType.Deluxe, hotels[1].ID),
                    new Room("Suite room with luxury amenities", RoomType.Suite, hotels[1].ID),
                    new Room("Cozy room with snow view", RoomType.Standart, hotels[2].ID),
                    new Room("Ski-in/ski-out suite", RoomType.Suite, hotels[2].ID),
                    new Room("Family chalet with kitchen", RoomType.Deluxe, hotels[3].ID),
                    new Room("Oasis view room", RoomType.Standart, hotels[4].ID),
                    new Room("Luxury tent with amenities", RoomType.Deluxe, hotels[5].ID),
                    new Room("Desert suite with panoramic view", RoomType.Suite, hotels[6].ID),
                    new Room("River view room", RoomType.Standart, hotels[7].ID),
                    new Room("Deluxe room with balcony", RoomType.Deluxe, hotels[8].ID),
                    new Room("Suite with river access", RoomType.Suite, hotels[9].ID)
                };

                context.Room.AddRange(rooms);
                context.SaveChanges();

                // Seed bookings with valID hotel and room references
                var bookings = new[]
                {
                    new Booking
                    {
                        CheckInDate = new DateOnly(2024, 5, 24),
                        CheckOutDate = new DateOnly(2024, 5, 27),
                        RoomId = rooms.FirstOrDefault(r => r.Summary == "Deluxe room with sea view").ID,
                        HotelId = hotels.FirstOrDefault(h => h.Name == "Grand Hotel").ID,
                        BreakfastRequests = 3
                    }
                };

                context.Booking.AddRange(bookings);
                context.SaveChanges();
            }
        }
    }
}
