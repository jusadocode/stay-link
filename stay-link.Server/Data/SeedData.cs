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
                if (context.Hotels.Any())
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

                context.Hotels.AddRange(hotels);
                context.SaveChanges();

                // Seed rooms and associate with hotels
                var rooms = new List<Room>
                {
                    new Room("Standard room with garden view", RoomType.Standart, 100, hotels[0].ID, 4),
                    new Room("Deluxe room with sea view", RoomType.Deluxe, 100, hotels[1].ID, 5),
                    new Room("Suite room with luxury amenities", RoomType.Suite, 120, hotels[1].ID, 3),
                    new Room("Cozy room with snow view", RoomType.Standart,130, hotels[2].ID, 2),
                    new Room("Ski-in/ski-out suite", RoomType.Suite,150, hotels[2].ID, 2),
                    new Room("Family chalet with kitchen", RoomType.Deluxe, 160, hotels[3].ID, 5),
                    new Room("Oasis view room", RoomType.Standart, 120, hotels[4].ID, 7),
                    new Room("Luxury tent with amenities", RoomType.Deluxe, 200, hotels[5].ID, 8),
                    new Room("Desert suite with panoramic view", RoomType.Suite,220, hotels[6].ID, 9),
                    new Room("River view room", RoomType.Standart, 180,hotels[7].ID, 2),
                    new Room("Deluxe room with balcony", RoomType.Deluxe,160,hotels[8].ID,3),
                    new Room("Suite with river access", RoomType.Suite,120,hotels[9].ID,2)
                };


                context.Rooms.AddRange(rooms);
                context.SaveChanges();

                var features = new[]
                {
                    new Feature { Name = "Free Wi-Fi", Description = "Complimentary wireless internet access." },
                    new Feature { Name = "Pool", Description = "Outdoor and indoor pools available." },
                    new Feature { Name = "Gym", Description = "Fully equipped gym for fitness enthusiasts." },
                    new Feature { Name = "Breakfast Included", Description = "Complimentary breakfast available." },
                    new Feature { Name = "Spa", Description = "Relax and rejuvenate with our spa services." },
                    new Feature { Name = "Parking", Description = "Free parking for guests." },
                    new Feature { Name = "Pet-Friendly", Description = "Pets allowed in designated rooms." },
                    new Feature { Name = "Room Service", Description = "24/7 room service for your convenience." },
                    new Feature { Name = "Conference Room", Description = "Available for business meetings and events." },
                    new Feature { Name = "Beachfront", Description = "Direct access to the beach." }
                };


                context.Feature.AddRange(features);
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
                        BreakfastRequests = 3,
                        TotalGuests = rooms.FirstOrDefault(r => r.Summary == "Deluxe room with sea view").MaxOccupancy - 1,
                        UserID = "4cdc02fe-5fef-4e2c-a022-fbf178d11112"
                    },
                    new Booking
                    {
                        CheckInDate = new DateOnly(2024, 6, 20),
                        CheckOutDate = new DateOnly(2024, 6, 21),
                        RoomId = rooms.FirstOrDefault(r => r.Summary == "Cozy room with snow view").ID,
                        HotelId = hotels.FirstOrDefault(h => h.Name == "Ocean Breeze Hotel").ID,
                        TotalGuests = rooms.FirstOrDefault(r => r.Summary == "Cozy room with snow view").MaxOccupancy - 1,
                        BreakfastRequests = 2,
                        UserID = "b3a703b8-0686-4fc6-a54c-69a6a9d91a7f"
                    }
                };

                context.Bookings.AddRange(bookings);
                context.SaveChanges();

                var roomFeatures = new[]
                {
                    new RoomFeature
                    {
                        RoomId = rooms.FirstOrDefault(r => r.Summary == "Deluxe room with sea view").ID.ToString(),
                        FeatureId = features.FirstOrDefault(f => f.Name == "Free Wi-Fi").ID.ToString(),
                        Condition = FeatureCondition.Great
                    },
                    new RoomFeature
                    {
                        RoomId = rooms.FirstOrDefault(r => r.Summary == "Standard room with garden view").ID.ToString(),
                        FeatureId = features.FirstOrDefault(f => f.Name == "Pool").ID.ToString(),
                        Condition = FeatureCondition.Great
                    },
                    new RoomFeature
                    {
                        RoomId = rooms.FirstOrDefault(r => r.Summary == "Suite room with luxury amenities").ID.ToString(),
                        FeatureId = features.FirstOrDefault(f => f.Name == "Spa").ID.ToString(),
                        Condition = FeatureCondition.Great
                    },
                    new RoomFeature
                    {
                        RoomId = rooms.FirstOrDefault(r => r.Summary == "Family chalet with kitchen").ID.ToString(),
                        FeatureId = features.FirstOrDefault(f => f.Name == "Breakfast Included").ID.ToString(),
                        Condition = FeatureCondition.Great
                    }
                };


                context.RoomFeature.AddRange(roomFeatures);
                context.SaveChanges();


            }
        }
    }
}
