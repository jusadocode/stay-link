using Xunit;
using Moq;
using stay_link.Server.Models;
using stay_link.Server.DTO;
using stay_link.Server.Services;
using stay_link.Server.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using stay_link.Server.Mappings;

namespace RoomServiceTests
{
    public class RoomMatchingTests
    {
        private async Task<BookingContext> GetInMemoryDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new BookingContext(options);
            await context.Database.EnsureCreatedAsync();

            var mapper = GetMapper();

            // Initialize RoomFeatures
            var featureBalcony = new RoomFeature { Id = 1, Name = "Balcony" };
            var featureAC = new RoomFeature { Id = 2, Name = "AC" };
            var featureSeaView = new RoomFeature { Id = 3, Name = "SeaView" };

            await context.RoomFeatures.AddRangeAsync(featureBalcony, featureAC, featureSeaView);
            await context.SaveChangesAsync();

            // Initialize RoomDTOs
            var roomDto1 = new RoomDTO
            {
                Title = "Room 101",
                Summary = "Nice room with balcony",
                RoomType = RoomType.Standart,
                Price = 100,
                HotelID = 1,
                MaxOccupancy = 2,
                Features = new List<RoomFeature> { featureBalcony }
            };

            var roomDto2 = new RoomDTO
            {
                Title = "Room 202",
                Summary = "Nice room with seaView",
                RoomType = RoomType.Standart,
                Price = 120,
                HotelID = 1,
                MaxOccupancy = 3,
                Features = new List<RoomFeature> { featureSeaView, featureBalcony, featureAC }
            };

            var roomDto3 = new RoomDTO
            {
                Title = "Room 303",
                Summary = "Nice room with AC",
                RoomType = RoomType.Standart,
                Price = 100,
                HotelID = 1,
                MaxOccupancy = 3,
                Features = new List<RoomFeature> { featureAC }
            };

            // Map RoomDTOs to Room entities
            var room1 = mapper.Map<Room>(roomDto1);
            var room2 = mapper.Map<Room>(roomDto2);
            var room3 = mapper.Map<Room>(roomDto3);

            // Initialize RoomUsages
            var roomUsage1 = new RoomUsage
            {
                RoomID = 1,
                CleaningState = CleaningState.Clean,
                GeneralWear = 0.6,
                TimesBookedThisYear = 1,
                TimesBookedSinceMaintenance = 1
            };

            var roomUsage2 = new RoomUsage
            {
                RoomID = 2,
                CleaningState = CleaningState.Clean,
                GeneralWear = 0.5,
                TimesBookedThisYear = 1,
                TimesBookedSinceMaintenance = 1
            };

            var roomUsage3 = new RoomUsage
            {
                RoomID = 3,
                CleaningState = CleaningState.Clean,
                GeneralWear = 0.4,
                TimesBookedThisYear = 1,
                TimesBookedSinceMaintenance = 1
            };

            // Add Rooms and RoomUsages to the database
            await context.Rooms.AddRangeAsync(room1, room2, room3);
            await context.RoomUsages.AddRangeAsync(roomUsage1, roomUsage2, roomUsage3);
            await context.SaveChangesAsync();

            return context;
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return config.CreateMapper();
        }

        [Fact]
        public async Task FindMatchingRoomsByPreference_ShouldReturnRoomsBasedOnScore()
        {
            // Arrange
            var dbContext = await GetInMemoryDbContextAsync();
            var mapper = GetMapper();
            var service = new RoomService(dbContext, mapper);

            var checkIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            var checkOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2));
            var guestCount = 2;
            var preferences = new List<RoomFeature> { new RoomFeature { Id = 2, Name = "AC" }, new RoomFeature { Id = 3, Name = "SeaView" } };

            // Act
            var result = await service.FindMatchingRoomsByPreference(checkIn, checkOut, guestCount, preferences);

            // Assert
            Assert.NotNull(result);
            var roomList = result.ToList();
            foreach (var room in roomList)
            {
                Console.WriteLine($"Room ID: {room.ID}");
                Console.WriteLine($"Title: {room.Title}");
                Console.WriteLine($"Summary: {room.Summary}");
                Console.WriteLine($"Price: {room.Price}");
                Console.WriteLine($"Max Occupancy: {room.MaxOccupancy}");
                Console.WriteLine("Features:");
                foreach (var feature in room.Features)
                {
                    Console.WriteLine($"  - {feature.Name}");
                }
                Console.WriteLine(); // Add a blank line for readability
            }
            Assert.Equal(2, roomList.Count); // Ensure only one room matches the preference
            Assert.Equal("Room 202", roomList[0].Title); // Room with AC should be returned
        }

        [Fact]
        public async Task FindMatchingRoomsByPreference_NoMatchingPreferences_ShouldReturnEmptyList()
        {
            // Arrange
            var dbContext = await GetInMemoryDbContextAsync();
            var mapper = GetMapper();
            var service = new RoomService(dbContext, mapper);

            var checkIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            var checkOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2));
            var guestCount = 2;
            var preferences = new List<RoomFeature> { new RoomFeature { Id = 99, Name = "NonExistentFeature" } }; // No room has this feature

            // Act
            var result = await service.FindMatchingRoomsByPreference(checkIn, checkOut, guestCount, preferences);

            // Assert
            Assert.NotNull(result);
            var roomList = result.ToList();
            Assert.Empty(roomList); // No rooms should match the preference
        }

        [Fact]
        public async Task FindMatchingRoomsByPreference_MultiplePreferences_ShouldReturnRoomsBasedOnScore()
        {
            // Arrange
            var dbContext = await GetInMemoryDbContextAsync();
            var mapper = GetMapper();
            var service = new RoomService(dbContext, mapper);

            var checkIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            var checkOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2));
            var guestCount = 2;
            var preferences = new List<RoomFeature> { new RoomFeature { Id = 2, Name = "AC" }, new RoomFeature { Id = 3, Name = "SeaView" } };

            // Act
            var result = await service.FindMatchingRoomsByPreference(checkIn, checkOut, guestCount, preferences);

            // Assert
            Assert.NotNull(result);
            var roomList = result.ToList();

            // Log room details for debugging
            foreach (var room in roomList)
            {
                Console.WriteLine($"Room ID: {room.ID}");
                Console.WriteLine($"Title: {room.Title}");
                Console.WriteLine($"Summary: {room.Summary}");
                Console.WriteLine($"Price: {room.Price}");
                Console.WriteLine($"Max Occupancy: {room.MaxOccupancy}");
                Console.WriteLine("Features:");
                foreach (var feature in room.Features)
                {
                    Console.WriteLine($"  - {feature.Name}");
                }
                Console.WriteLine(); // Add a blank line for readability
            }

            Assert.Equal(2, roomList.Count); // Only Room 202 and Room 303 should match
            Assert.Equal("Room 202", roomList[0].Title); // Room 202 has both AC and SeaView
            Assert.Equal("Room 303", roomList[1].Title); // Room 303 has only AC
        }

        [Fact]
        public async Task FindMatchingRoomsByPreference_RoomsWithNoFeatures_ShouldReturnRoomsWithoutFeatures()
        {
            // Arrange
            var dbContext = await GetInMemoryDbContextAsync();
            var mapper = GetMapper();
            var service = new RoomService(dbContext, mapper);

            // Add a room with no features
            var roomDto4 = new RoomDTO
            {
                Title = "Room 404",
                Summary = "Basic room with no features",
                RoomType = RoomType.Standart,
                Price = 80,
                HotelID = 1,
                MaxOccupancy = 2,
                Features = new List<RoomFeature>() // No features
            };

            var roomUsage4 = new RoomUsage
            {
                RoomID = 4,
                CleaningState = CleaningState.Clean,
                GeneralWear = 0.3,
                TimesBookedThisYear = 30,
                TimesBookedSinceMaintenance = 40
            };

            var room4 = mapper.Map<Room>(roomDto4);
            await dbContext.Rooms.AddAsync(room4);
            await dbContext.RoomUsages.AddAsync(roomUsage4);
            await dbContext.SaveChangesAsync();

            var checkIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            var checkOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2));
            var guestCount = 2;
            var preferences = new List<RoomFeature>(); // No preferences

            // Act
            var result = await service.FindMatchingRoomsByPreference(checkIn, checkOut, guestCount, preferences);

            // Assert
            Assert.NotNull(result);
            var roomList = result.ToList();
            Assert.Equal(4, roomList.Count); // All rooms should be returned, including the one with no features
        }

        [Fact]
        public async Task FindMatchingRoomsByPreference_RoomsWithDifferentWearLevels_ShouldReturnRoomsBasedOnScoreAndWear()
        {
            // Arrange
            var dbContext = await GetInMemoryDbContextAsync();
            var mapper = GetMapper();
            var service = new RoomService(dbContext, mapper);

            // Update room usage to simulate different wear levels
            var roomUsage3 = await dbContext.RoomUsages.FirstOrDefaultAsync(ru => ru.RoomID == 3);
            roomUsage3.GeneralWear = 0.8; // Higher wear level for Room 303
            await dbContext.SaveChangesAsync();

            var checkIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            var checkOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2));
            var guestCount = 2;
            var preferences = new List<RoomFeature> { new RoomFeature { Id = 2, Name = "AC" } };

            // Act
            var result = await service.FindMatchingRoomsByPreference(checkIn, checkOut, guestCount, preferences);

            // Assert
            Assert.NotNull(result);
            var roomList = result.ToList();
            Assert.Equal(2, roomList.Count); // Room 202 and Room 303 should match
            Assert.Equal("Room 202", roomList[0].Title); // Room 202 has lower wear
            Assert.Equal("Room 303", roomList[1].Title); // Room 303 has higher wear
        }
    }
}