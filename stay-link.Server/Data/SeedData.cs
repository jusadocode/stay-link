using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using stay_link.Server.Models.Auth;
using stay_link.Server.Models.Bookings;
using stay_link.Server.Models.Enums;
using stay_link.Server.Models.RoomOperations;
using stay_link.Server.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace stay_link.Server.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookingContext(serviceProvider.GetRequiredService<DbContextOptions<BookingContext>>()))
            {

                // Ensure the database is created.
                context.Database.EnsureCreated();

                // Check if any hotels exist.
                if (context.Rooms.Any())
                {
                    return; // Database has been seeded
                }

                var userManager = serviceProvider.GetRequiredService<UserManager<BookingUser>>();

                var user1 = await userManager.FindByEmailAsync("johnm@gmail.com");
                var user2 = await userManager.FindByEmailAsync("hughes@gmail.com");

                using var transaction = await context.Database.BeginTransactionAsync();
                try
                {
                    var roomFeatures = new[]
                                    {
                        // Basic Comforts
                        new RoomFeature { Name = "Air Conditioning", Description = "Individually controlled AC." },
                        new RoomFeature { Name = "Ensuite Bathroom", Description = "Private bathroom with shower/tub." },
                        new RoomFeature { Name = "Free WiFi", Description = "Complimentary high-speed internet access." },
                        new RoomFeature { Name = "Flat-screen TV", Description = "Television with cable/satellite channels." },
                        new RoomFeature { Name = "Hair Dryer", Description = "Available in the bathroom." },
                        // Views & Location
                        new RoomFeature { Name = "Balcony", Description = "Private balcony or terrace." },
                        new RoomFeature { Name = "Lake View", Description = "Room overlooks the lake." },
                        new RoomFeature { Name = "City View", Description = "Room overlooks the city skyline." },
                        new RoomFeature { Name = "Quiet Side", Description = "Room located away from main roads/noise." }, 
                         // Bed Types
                        new RoomFeature { Name = "King Bed", Description = "Room features a king-sized bed." },
                        new RoomFeature { Name = "Queen Bed", Description = "Room features a queen-sized bed." },
                        new RoomFeature { Name = "Twin Beds", Description = "Room features two separate single beds." }, 
                        // Amenities
                        new RoomFeature { Name = "Mini Bar", Description = "Stocked mini-bar (charges may apply)." },
                        new RoomFeature { Name = "Coffee Maker", Description = "In-room coffee/tea making facilities." },
                        new RoomFeature { Name = "Work Desk", Description = "Dedicated desk space for work." },
                        new RoomFeature { Name = "Sofa Bed", Description = "Includes a sofa that converts to a bed." }, 
                        // Special
                        new RoomFeature { Name = "Accessible", Description = "Room designed for accessibility needs." },
                        new RoomFeature { Name = "Pet Friendly", Description = "Pets are allowed (check policy/fees)." },
                        new RoomFeature { Name = "Jacuzzi Tub", Description = "Bathroom includes a jacuzzi tub." }
                    };

                    context.RoomFeatures.AddRange(roomFeatures);
                    context.SaveChanges();

                    var rooms = new List<Room>
                    {
                        new Room { Title = "Standard Lake View", Summary = "Comfortable room with beautiful lake views.", RoomType = RoomType.Standard, Price = 150,  MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4], roomFeatures[6] }, ImageUrl = "https://images.unsplash.com/photo-1512918728675-ed5a9ecdebfd?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Deluxe Balcony Room", Summary = "Spacious deluxe room with private balcony.", RoomType = RoomType.Deluxe, Price = 220,  MaxOccupancy = 3, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4], roomFeatures[5], roomFeatures[9], roomFeatures[13] }, ImageUrl = "https://images.unsplash.com/photo-1568495248636-6432b97bd949?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Family Lake Suite", Summary = "Large suite perfect for families, stunning lake views.", RoomType = RoomType.Family, Price = 350,  MaxOccupancy = 5, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4], roomFeatures[5], roomFeatures[6], roomFeatures[10], roomFeatures[13], roomFeatures[15] }, ImageUrl = "https://images.unsplash.com/photo-1611892440504-42a792e24d32?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Accessible Twin Room", Summary = "Accessible room with two twin beds and garden view.", RoomType = RoomType.Twin, Price = 140, MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4], roomFeatures[11], roomFeatures[16] }, ImageUrl = "https://images.unsplash.com/photo-1631049307264-da0ec9d70304?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" },

                        new Room { Title = "Executive King", Summary = "High-floor executive room with king bed and city view.", RoomType = RoomType.Executive, Price = 280,  MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4], roomFeatures[7], roomFeatures[9], roomFeatures[12], roomFeatures[13], roomFeatures[14] }, ImageUrl = "https://images.unsplash.com/photo-1596394516093-501ba68a0ba6?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Standard Queen", Summary = "Well-appointed standard room with a queen bed.", RoomType = RoomType.Standard, Price = 180, MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4], roomFeatures[10] }, ImageUrl = "https://images.unsplash.com/photo-1598928636135-d146006ff4be?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "City View Suite", Summary = "Luxurious suite with panoramic city views and jacuzzi.", RoomType = RoomType.Suite, Price = 450,  MaxOccupancy = 3, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4], roomFeatures[5], roomFeatures[7], roomFeatures[9], roomFeatures[12], roomFeatures[13], roomFeatures[14], roomFeatures[15], roomFeatures[18] }, ImageUrl = "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Double Room", Summary = "Comfortable room with a standard double bed.", RoomType = RoomType.Double, Price = 190,  MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4] }, ImageUrl = "https://images.unsplash.com/photo-1618773928121-c32242e63f39?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 

                        new Room { Title = "Single Basic", Summary = "A small, clean room for the solo traveler.", RoomType = RoomType.Single, Price = 70, MaxOccupancy = 1, Features = new List<RoomFeature> { roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4] }, ImageUrl = "https://images.unsplash.com/photo-1594136539708-d3a2d5f15468?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Quiet Twin Room", Summary = "A peaceful room with two single beds.", RoomType = RoomType.Twin, Price = 95, MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4], roomFeatures[8], roomFeatures[11] }, ImageUrl = "https://images.unsplash.com/photo-1590490360182-c33d57733427?q=80&w=1994&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Pet Friendly Standard", Summary = "Standard room where pets are welcome.", RoomType = RoomType.Standard, Price = 90,  MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4], roomFeatures[17] }, ImageUrl = "https://images.unsplash.com/photo-1598605272254-cd674adec494?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Triple Room", Summary = "Basic room suitable for three guests.", RoomType = RoomType.Triple, Price = 110, MaxOccupancy = 3, Features = new List<RoomFeature> { roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4] }, ImageUrl = "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 

                        new Room { Title = "Rustic Cabin Escape", Summary = "Cozy cabin-style room with wooden interiors and fireplace.", RoomType = RoomType.Deluxe, Price = 240, MaxOccupancy = 3, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[14], roomFeatures[15], roomFeatures[5] }, ImageUrl = "https://images.unsplash.com/photo-1570129477492-45c003edd2be?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Modern Loft", Summary = "Stylish loft-style room with city skyline views.", RoomType = RoomType.Executive, Price = 300, MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[2], roomFeatures[3], roomFeatures[7], roomFeatures[9], roomFeatures[14] }, ImageUrl = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Coastal View Room", Summary = "Bright room overlooking the coast, ideal for relaxation.", RoomType = RoomType.Standard, Price = 160, MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[2], roomFeatures[3], roomFeatures[6], roomFeatures[13], roomFeatures[17] }, ImageUrl = "https://images.unsplash.com/photo-1559599238-cb4c26dfea5c?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Classic Queen Garden", Summary = "Traditional room with garden-facing views and queen bed.", RoomType = RoomType.Standard, Price = 175, MaxOccupancy = 2, Features = new List<RoomFeature> { roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[10], roomFeatures[8] }, ImageUrl = "https://images.unsplash.com/photo-1523217582562-09d0def993a6?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Urban Minimal Single", Summary = "Minimalist single room with modern design.", RoomType = RoomType.Single, Price = 85, MaxOccupancy = 1, Features = new List<RoomFeature> { roomFeatures[2], roomFeatures[3], roomFeatures[4] }, ImageUrl = "https://images.unsplash.com/photo-1622633634575-22c6f77f6e1d?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                        new Room { Title = "Luxury Suite - Jacuzzi Bliss", Summary = "Elegant suite with full amenities and private jacuzzi tub.", RoomType = RoomType.Suite, Price = 500, MaxOccupancy = 4, Features = new List<RoomFeature> { roomFeatures[0], roomFeatures[1], roomFeatures[2], roomFeatures[3], roomFeatures[4], roomFeatures[5], roomFeatures[9], roomFeatures[12], roomFeatures[13], roomFeatures[14], roomFeatures[18] }, ImageUrl = "https://images.unsplash.com/photo-1600585152630-988d4b7b0a33?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3" }, 
                    };


                    context.Rooms.AddRange(rooms);
                    context.SaveChanges();



                    var bookings = new List<Booking>();

                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 3, 10), CheckOutDate = new DateOnly(2025, 3, 13), Rooms = new List<Room> { rooms[0] }, TotalGuests = 1, BreakfastRequests = 1, UserId = user1.Id, DisplayName = user1.FirstName, CreationTime = DateTime.UtcNow.AddDays(-40) });
                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 4, 9), CheckOutDate = new DateOnly(2025, 4, 14), Rooms = new List<Room> { rooms[1] }, TotalGuests = 2, BreakfastRequests = 2, UserId = user2.Id, DisplayName = user2.FirstName, CreationTime = DateTime.UtcNow.AddDays(-5) });
                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 5, 1), CheckOutDate = new DateOnly(2025, 5, 5), Rooms = new List<Room> { rooms[2] }, TotalGuests = 2, BreakfastRequests = 0, UserId = user1.Id, DisplayName = user1.FirstName, CreationTime = DateTime.UtcNow.AddDays(-2) });
                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 6, 15), CheckOutDate = new DateOnly(2025, 6, 20), Rooms = new List<Room> { rooms[4], rooms[5] }, TotalGuests = 4, BreakfastRequests = 4, UserId = user1.Id, DisplayName = user1.FirstName, CreationTime = DateTime.UtcNow.AddDays(-1) });
                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 4, 25), CheckOutDate = new DateOnly(2025, 4, 28), Rooms = new List<Room> { rooms[6] }, TotalGuests = 2, BreakfastRequests = 0, UserId = user2.Id, DisplayName = user2.FirstName, CreationTime = DateTime.UtcNow });
                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 5, 20), CheckOutDate = new DateOnly(2025, 5, 22), Rooms = new List<Room> { rooms[9] }, TotalGuests = 2, BreakfastRequests = 2, UserId = user1.Id, DisplayName = user1.FirstName, CreationTime = DateTime.UtcNow });
                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 2, 1), CheckOutDate = new DateOnly(2025, 2, 5), Rooms = new List<Room> { rooms[10] }, TotalGuests = 1, BreakfastRequests = 0, UserId = user2.Id, DisplayName = user2.FirstName, CreationTime = DateTime.UtcNow.AddDays(-70) });

                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 3, 1), CheckOutDate = new DateOnly(2025, 3, 3), Rooms = new List<Room> { rooms[11] }, TotalGuests = 2, BreakfastRequests = 2, UserId = user1.Id, DisplayName = user1.FirstName, CreationTime = DateTime.UtcNow.AddDays(-50) });
                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 4, 16), CheckOutDate = new DateOnly(2025, 4, 20), Rooms = new List<Room> { rooms[12] }, TotalGuests = 2, BreakfastRequests = 1, UserId = user2.Id, DisplayName = user2.FirstName, CreationTime = DateTime.UtcNow.AddDays(-1) });
                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 4, 18), CheckOutDate = new DateOnly(2025, 4, 22), Rooms = new List<Room> { rooms[13] }, TotalGuests = 2, BreakfastRequests = 0, UserId = user1.Id, DisplayName = user1.FirstName, CreationTime = DateTime.UtcNow });
                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 5, 10), CheckOutDate = new DateOnly(2025, 5, 15), Rooms = new List<Room> { rooms[14] }, TotalGuests = 2, BreakfastRequests = 2, UserId = user2.Id, DisplayName = user2.FirstName, CreationTime = DateTime.UtcNow });
                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 6, 5), CheckOutDate = new DateOnly(2025, 6, 8), Rooms = new List<Room> { rooms[15] }, TotalGuests = 1, BreakfastRequests = 0, UserId = user1.Id, DisplayName = user1.FirstName, CreationTime = DateTime.UtcNow });
                    bookings.Add(new Booking { CheckInDate = new DateOnly(2025, 12, 24), CheckOutDate = new DateOnly(2025, 12, 28), Rooms = new List<Room> { rooms[16] }, TotalGuests = 4, BreakfastRequests = 4, UserId = user2.Id, DisplayName = user2.FirstName, CreationTime = DateTime.UtcNow.AddDays(-1) });

                    context.Bookings.AddRange(bookings);
                    context.SaveChanges();

                    // Seed RoomUsage data
                    var roomUsages = new List<RoomUsage>();
                    var random = new Random();
                    foreach (var room in rooms)
                    {
                        roomUsages.Add(new RoomUsage
                        {
                            RoomId = room.Id,
                            // Random initial wear between 5% and 35%
                            GeneralWear = Math.Round(random.NextDouble() * 0.30 + 0.05, 2),
                            // Mostly clean, occasionally needs cleaning
                            CleaningState = random.Next(10) < 8 ? CleaningState.Clean : CleaningState.NeedsCleaning,
                            TimesBookedThisYear = random.Next(2, 15),
                            TimesBookedSinceMaintenance = random.Next(0, 8)
                        });
                    }

                    context.RoomUsages.AddRange(roomUsages);
                    context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("Seeding failed: " + ex.Message);
                    throw;
                }
            }

        }
    }
}