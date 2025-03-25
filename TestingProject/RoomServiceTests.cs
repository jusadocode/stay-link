using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using stay_link.Server.Data;
using stay_link.Server.Models;
using stay_link.Server.Services;
using stay_link.Server.DTO;
using AutoMapper;
using stay_link.Server.Mappings;

namespace RoomServiceTests
{
    public class RoomServiceTests
    {
        private async Task<BookingContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new BookingContext(options);
            databaseContext.Database.EnsureCreated();

            if (!await databaseContext.Rooms.AnyAsync())
            {
                var hotel = new Hotel { ID = 1, Name = "Test Hotel", Address = "123 Test St", ImageUrl = "test.jpg" };
                await databaseContext.Hotels.AddAsync(hotel);

                await databaseContext.Rooms.AddRangeAsync(new List<Room>
                {
                    new Room { ID = 1, Title = "Room 101", MaxOccupancy = 2, HotelID = 1, Price = 100 },
                    new Room { ID = 2, Title = "Room 102", MaxOccupancy = 3, HotelID = 1, Price = 150 }
                });

                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }
        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            return config.CreateMapper();
        }

        [Fact]
        public async Task GetRooms_ShouldReturnAllRooms()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var mockMapper = GetMapper();
            var service = new RoomService(dbContext, mockMapper);

            // Act
            var rooms = await service.GetRooms();

            // Assert
            Assert.Equal(2, rooms.Count());
        }

        [Fact]
        public async Task GetRoom_ExistingId_ShouldReturnRoom()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var mockMapper = GetMapper();
            var service = new RoomService(dbContext, mockMapper);

            // Act
            var room = await service.GetRoom(1);

            // Assert
            Assert.NotNull(room);
            Assert.Equal("Room 101", room.Title);
        }

        [Fact]
        public async Task GetRoom_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var mockMapper = GetMapper();
            var service = new RoomService(dbContext, mockMapper);

            // Act
            var room = await service.GetRoom(99);

            // Assert
            Assert.Null(room);
        }

        [Fact]
        public async Task CreateRoom_ValidRoom_ShouldReturnRoom()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var mockMapper = GetMapper();
            var service = new RoomService(dbContext, mockMapper);

            var newRoomDTO = new CreateRoomDTO
            {
                Title = "Room 103",
                MaxOccupancy = 4,
                HotelID = 1,
                Price = 200
            };

            // Act
            var createdRoom = await service.CreateRoom(newRoomDTO);

            // Assert
            Assert.NotNull(createdRoom);
            Assert.Equal("Room 103", createdRoom.Title);
            Assert.Equal(3, (await service.GetRooms()).Count());
        }

        [Fact]
        public async Task CreateRoom_InvalidHotel_ShouldThrowException()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var mockMapper = GetMapper();
            var service = new RoomService(dbContext, mockMapper);

            var invalidRoomDTO = new CreateRoomDTO
            {
                Title = "Room 200",
                MaxOccupancy = 2,
                HotelID = 99,
                Price = 180
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => service.CreateRoom(invalidRoomDTO));
            Assert.Equal("Specified hotel does not exist.", ex.Message);
        }

        [Fact]
        public async Task UpdateRoom_ExistingRoom_ShouldUpdateSuccessfully()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var mockMapper = GetMapper();
            var service = new RoomService(dbContext, mockMapper);

            var updatedRoomDTO = new UpdateRoomDTO
            {
                Title = "Updated Room 101",
                MaxOccupancy = 3,
                HotelID = 1,
                Price = 120
            };

            // Act
            var result = await service.UpdateRoom(1, updatedRoomDTO);

            // Assert
            Assert.True(result);
            var room = await service.GetRoom(1);
            Assert.Equal("Updated Room 101", room.Title);
        }

        [Fact]
        public async Task UpdateRoom_NonExistingRoom_ShouldReturnFalse()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var mockMapper = GetMapper();
            var service = new RoomService(dbContext, mockMapper);

            var nonExistingRoomDTO = new UpdateRoomDTO
            {
                Title = "Non-Existing",
                MaxOccupancy = 2,
                HotelID = 1,
                Price = 100
            };

            // Act
            var result = await service.UpdateRoom(99, nonExistingRoomDTO);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteRoom_ExistingRoom_ShouldReturnTrue()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var mockMapper = GetMapper();
            var service = new RoomService(dbContext, mockMapper);

            // Act
            var result = await service.DeleteRoom(1);
            var room = await service.GetRoom(1);

            // Assert
            Assert.True(result);
            Assert.Null(room);
        }

        [Fact]
        public async Task DeleteRoom_NonExistingRoom_ShouldReturnFalse()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var mockMapper = GetMapper();
            var service = new RoomService(dbContext, mockMapper);

            // Act
            var result = await service.DeleteRoom(99);

            // Assert
            Assert.False(result);
        }


    }
}