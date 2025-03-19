using Microsoft.EntityFrameworkCore;
using stay_link.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stay_link.Server.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookingContext(
                serviceProvider.GetRequiredService<DbContextOptions<BookingContext>>()))
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
                    new Hotel { Name = "Grand Hotel", Address = "123 Main St, Springfield", ImageUrl = "https://example.com/grand-hotel.jpg" },
                    new Hotel { Name = "Ocean Breeze Hotel", Address = "456 Ocean Ave, Miami", ImageUrl = "https://example.com/ocean-breeze.jpg" },
                    new Hotel { Name = "Purple Forest Hotel", Address = "12 Wolf Street, Portland", ImageUrl = "https://example.com/purple-forest.jpg" },
                    new Hotel { Name = "Mountain View Inn", Address = "789 Mountain Rd, Denver", ImageUrl = "https://example.com/mountain-view.jpg" },
                    new Hotel { Name = "City Lights Hotel", Address = "101 City Center Blvd, New York", ImageUrl = "https://example.com/city-lights.jpg" },
                    new Hotel { Name = "Sunset Resort", Address = "202 Sunset Dr, Malibu", ImageUrl = "https://example.com/sunset-resort.jpg" },
                    new Hotel { Name = "Forest Retreat", Address = "303 Forest Ln, Asheville", ImageUrl = "https://example.com/forest-retreat.jpg" },
                    new Hotel { Name = "Riverside Hotel", Address = "404 River St, Chicago", ImageUrl = "https://example.com/riverside-hotel.jpg" },
                    new Hotel { Name = "Desert Oasis", Address = "505 Desert Rd, Phoenix", ImageUrl = "https://example.com/desert-oasis.jpg" },
                    new Hotel { Name = "Snowy Peaks Hotel", Address = "606 Snowy Rd, Aspen", ImageUrl = "https://example.com/snowy-peaks.jpg" }
                };

                context.Hotels.AddRange(hotels);
                context.SaveChanges();

                var roomFeatures = new[]
                {
                    new RoomFeature { Name = "Balcony", Description = "Room with a balcony." },
                    new RoomFeature { Name = "Sea View", Description = "Room with a view of the sea." },
                    new RoomFeature { Name = "Double Bed", Description = "Room with a double bed." },
                    new RoomFeature { Name = "Workplace", Description = "Room with a dedicated workspace." },
                    new RoomFeature { Name = "Quiet Side", Description = "Room located on the quiet side of the building." },
                    new RoomFeature { Name = "AC", Description = "Room with air conditioning." }
                };

                context.RoomFeatures.AddRange(roomFeatures);
                context.SaveChanges();

                // Seed rooms and associate with hotels
                var rooms = new List<Room>
                {
                    new Room { Title = "Standard Room", Summary = "Standard room with garden view", RoomType = RoomType.Standart, Price = 100, HotelID = hotels[0].ID, MaxOccupancy = 4, Features = new List<RoomFeature> { roomFeatures[5] } }, // AC
                    new Room { Title = "Deluxe Room", Summary = "Deluxe room with sea view", RoomType = RoomType.Deluxe, Price = 200, HotelID = hotels[1].ID, MaxOccupancy = 5, Features = new List<RoomFeature> { roomFeatures[1], roomFeatures[5] } }, // Sea View, AC
                    new Room { Title = "Suite", Summary = "Suite room with luxury amenities", RoomType = RoomType.Suite, Price = 300, HotelID = hotels[1].ID, MaxOccupancy = 3, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[2], roomFeatures[5] } }, // Balcony, Double Bed, AC
                    new Room { Title = "Cozy Room", Summary = "Cozy room with snow view", RoomType = RoomType.Standart, Price = 130, HotelID = hotels[2].ID, MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[5] } }, // AC
                    new Room { Title = "Ski Suite", Summary = "Ski-in/ski-out suite", RoomType = RoomType.Suite, Price = 150, HotelID = hotels[2].ID, MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[5] } }, // Balcony, AC
                    new Room { Title = "Family Chalet", Summary = "Family chalet with kitchen", RoomType = RoomType.Deluxe, Price = 160, HotelID = hotels[3].ID, MaxOccupancy = 5, Features = new List<RoomFeature> { roomFeatures[2], roomFeatures[5] } }, // Double Bed, AC
                    new Room { Title = "Oasis Room", Summary = "Oasis view room", RoomType = RoomType.Standart, Price = 120, HotelID = hotels[4].ID, MaxOccupancy = 7, Features = new List<RoomFeature> { roomFeatures[5] } }, // AC
                    new Room { Title = "Luxury Tent", Summary = "Luxury tent with amenities", RoomType = RoomType.Deluxe, Price = 200, HotelID = hotels[5].ID, MaxOccupancy = 8, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[5] } }, // Balcony, AC
                    new Room { Title = "Desert Suite", Summary = "Desert suite with panoramic view", RoomType = RoomType.Suite, Price = 220, HotelID = hotels[6].ID, MaxOccupancy = 9, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[5] } }, // Balcony, AC
                    new Room { Title = "River View Room", Summary = "River view room", RoomType = RoomType.Standart, Price = 180, HotelID = hotels[7].ID, MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[5] } }, // Balcony, AC
                    new Room { Title = "Balcony Room", Summary = "Deluxe room with balcony", RoomType = RoomType.Deluxe, Price = 160, HotelID = hotels[8].ID, MaxOccupancy = 3, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[5] } }, // Balcony, AC
                    new Room { Title = "River Access Suite", Summary = "Suite with river access", RoomType = RoomType.Suite, Price = 120, HotelID = hotels[9].ID, MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[5] } } // Balcony, AC
                };


                context.Rooms.AddRange(rooms);
                context.SaveChanges();



                // Seed bookings with valid hotel and room references
                var bookings = new[]
                {
                    new Booking
                    {
                        CheckInDate = new DateOnly(2024, 5, 24),
                        CheckOutDate = new DateOnly(2024, 5, 27),
                        RoomId = rooms.First(r => r.Summary == "Deluxe room with sea view").ID,
                        HotelId = hotels.First(h => h.Name == "Ocean Breeze Hotel").ID,
                        BreakfastRequests = 3,
                        TotalGuests = rooms.First(r => r.Summary == "Deluxe room with sea view").MaxOccupancy - 1,
                        UserID = "025eac89-a267-46dc-8ab6-64b2292e63f1"
                    },
                    new Booking
                    {
                        CheckInDate = new DateOnly(2024, 6, 20),
                        CheckOutDate = new DateOnly(2024, 6, 21),
                        RoomId = rooms.First(r => r.Summary == "Cozy room with snow view").ID,
                        HotelId = hotels.First(h => h.Name == "Purple Forest Hotel").ID,
                        TotalGuests = rooms.First(r => r.Summary == "Cozy room with snow view").MaxOccupancy - 1,
                        BreakfastRequests = 2,
                        UserID = "b3a703b8-0686-4fc6-a54c-69a6a9d91a7f"
                    },
                    new Booking
                    {
                        CheckInDate = new DateOnly(2024, 7, 15),
                        CheckOutDate = new DateOnly(2024, 7, 18),
                        RoomId = rooms.First(r => r.Summary == "Family chalet with kitchen").ID,
                        HotelId = hotels.First(h => h.Name == "Mountain View Inn").ID,
                        TotalGuests = rooms.First(r => r.Summary == "Family chalet with kitchen").MaxOccupancy,
                        BreakfastRequests = 4,
                        UserID = "b3a703b8-0686-4fc6-a54c-69a6a9d91a7f"
                    },
                    new Booking
                    {
                        CheckInDate = new DateOnly(2024, 8, 10),
                        CheckOutDate = new DateOnly(2024, 8, 12),
                        RoomId = rooms.First(r => r.Summary == "Luxury tent with amenities").ID,
                        HotelId = hotels.First(h => h.Name == "Sunset Resort").ID,
                        TotalGuests = rooms.First(r => r.Summary == "Luxury tent with amenities").MaxOccupancy - 2,
                        BreakfastRequests = 1,
                        UserID = "b3a703b8-0686-4fc6-a54c-69a6a9d91a7f"
                    }
                };

                context.Bookings.AddRange(bookings);
                context.SaveChanges();

                // Seed RoomUsage data
                var roomUsages = new[]
                {
                    new RoomUsage { RoomID = rooms[0].ID, GeneralWear = 0.1, CleaningState = CleaningState.Clean, TimesBookedThisYear = 5, TimesBookedSinceMaintenance = 2 },
                    new RoomUsage { RoomID = rooms[1].ID, GeneralWear = 0.05, CleaningState = CleaningState.NeedsCleaning, TimesBookedThisYear = 3, TimesBookedSinceMaintenance = 1 },
                    new RoomUsage { RoomID = rooms[2].ID, GeneralWear = 0.2, CleaningState = CleaningState.BeingCleaned, TimesBookedThisYear = 10, TimesBookedSinceMaintenance = 5 },
                    new RoomUsage { RoomID = rooms[3].ID, GeneralWear = 0.15, CleaningState = CleaningState.Clean, TimesBookedThisYear = 7, TimesBookedSinceMaintenance = 3 }
                };

                context.RoomUsages.AddRange(roomUsages);
                context.SaveChanges();
            }
        }
    }
}