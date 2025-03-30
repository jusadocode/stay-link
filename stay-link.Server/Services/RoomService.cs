using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Data;
using stay_link.Server.DTO;
using stay_link.Server.Models;

namespace stay_link.Server.Services
{
    public class RoomService
    {
        private readonly BookingContext _context;
        private readonly IMapper _mapper;

        public RoomService(BookingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomDTO>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return _mapper.Map<IEnumerable<RoomDTO>>(rooms);
        }

        public async Task<IEnumerable<RoomDTO>> GetRooms(DateOnly checkIn, DateOnly checkOut, int guestCount, List<RoomFeature> preferences)
        {
            var rooms = await FindMatchingRoomsByPreference(checkIn, checkOut, guestCount, preferences);
            return _mapper.Map<IEnumerable<RoomDTO>>(rooms);
        }

        public async Task<RoomDTO?> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            return _mapper.Map<RoomDTO>(room);
        }

        public async Task<Room> CreateRoom(CreateRoomDTO createRoomDTO)
        {
            var room = _mapper.Map<Room>(createRoomDTO);

            var hotel = await _context.Hotels.FindAsync(room.HotelID);
            if (hotel == null)
                throw new Exception("Specified hotel does not exist.");

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<bool> UpdateRoom(int id, UpdateRoomDTO roomDTO)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return false;

            var hotel = await _context.Hotels.FindAsync(roomDTO.HotelID);
            if (hotel == null)
                throw new Exception("Specified hotel does not exist.");

            _mapper.Map(roomDTO, room); // Update room properties from DTO
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return false;

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RoomExists(int id)
        {
            return await _context.Rooms.AnyAsync(e => e.ID == id);
        }

        public async Task<List<RoomFeature>> GetRoomFeaturesByIds(List<int> preferenceIds)
        {
            var preferences = await _context.RoomFeatures
                                  .Where(f => preferenceIds.Contains(f.Id))
                                  .ToListAsync();
            return preferences;
        }

        public async Task<List<RoomFeatureDetailsDTO>> GetAllFeatures()
        {
            var features = await _context.RoomFeatures
                .Include(f => f.Bookings)
                .Include(f => f.Rooms)
                .ToListAsync();

            return _mapper.Map<List<RoomFeatureDetailsDTO>>(features);

        }

        public async Task <List<Room?>> FindMatchingRoomsByPreference(DateOnly checkIn, DateOnly checkOut, int guestCount, List<RoomFeature> preferences)
        {
            var availableRooms = await _context.Rooms
                .Where(r => !r.Bookings.Any(b => b.CheckInDate < checkOut && checkIn < b.CheckOutDate))
                .Where(r => r.MaxOccupancy >= guestCount)
                .Where(r => r.RoomUsage.CleaningState == CleaningState.Clean) // Ensure room is cleaned
                .OrderBy(r => r.RoomUsage.GeneralWear) // Prefer rooms with lower wear
                .ToListAsync();

            if (!availableRooms.Any())
                return null;

            var roomScores = new Dictionary<Room, double>();

            int prefCount = preferences.Count;

            foreach (var room in availableRooms)
            {
                double score = 0;
                int sumOfFeatureScores = 0;

                if (preferences != null)
                {
                    for (int i = 0; i < prefCount; i++)
                    {
                        int currentFeatureWeight = prefCount - i;
                        sumOfFeatureScores += currentFeatureWeight;
                        RoomFeature currentFeature = preferences[i];

                        if (room.Features.Any(f => f.Id == currentFeature.Id))
                        {
                            score += currentFeatureWeight;
                        }
                    }
                }

                double preferenceScore = score / sumOfFeatureScores;

                if (preferenceScore <= 0 && preferences.Count > 0)
                    continue;

                double maintenanceScore = 1 - room.RoomUsage.GeneralWear;

                double sizeMatchScore = 1 - ((room.MaxOccupancy - guestCount) / (double)room.MaxOccupancy);

                double finalRoomScore = 0.6 * preferenceScore + 0.2 * maintenanceScore + 0.2 * sizeMatchScore;

                Console.WriteLine(room.Title + ' ' +  finalRoomScore);
                Console.WriteLine("Preference score: " + preferenceScore);
                Console.WriteLine("Maintenance score: " + maintenanceScore);
                Console.WriteLine("GuestCount score: " + sizeMatchScore);

                roomScores[room] = finalRoomScore;

            }

            return roomScores.OrderByDescending(r => r.Value).Select(r => r.Key).Take(5).ToList();
        }

        public async Task<IEnumerable<Room?>> FindMatchingRoomsByBookingPreference(DateOnly checkIn, DateOnly checkOut, int guestCount, List<RoomFeature> preferences, List<BookingFeature> bookingPreferences)
        {
            var availableRooms = await _context.Rooms
                .Where(r => !r.Bookings.Any(b => b.CheckInDate < checkOut && checkIn < b.CheckOutDate))
                .Where(r => r.MaxOccupancy >= guestCount)
                .Where(r => r.RoomUsage.CleaningState == CleaningState.Clean) // Ensure room is cleaned
                .OrderBy(r => r.RoomUsage.GeneralWear) // Prefer rooms with lower wear
                .ToListAsync();

            if (!availableRooms.Any())
                return null;

            var roomScores = new Dictionary<Room, int>();

            foreach (var room in availableRooms)
            {
                int score = 0;

                if (preferences != null)
                {
                    foreach (var roomFeature in preferences)
                    {
                        if (preferences.Contains(roomFeature))
                        {
                            score += 8;
                        }
                    }
                }

                if (room.RoomUsage.GeneralWear < 0.5)
                    score += 5;

                if (room.MaxOccupancy == guestCount)
                    score += 3;

                roomScores[room] = score;
            }

            return roomScores.OrderByDescending(r => r.Value).Select(r => r.Key).Take(5).ToList();
        }

        public async Task UpdateRoomUsageAfterBooking(int roomId, int numberOfGuests, int stayDuration)
        {
            var roomUsage = await _context.RoomUsages.FirstOrDefaultAsync(ru => ru.RoomID == roomId);
            if (roomUsage == null)
            {
                throw new InvalidOperationException("Room usage record not found.");
            }

            roomUsage.TimesBookedThisYear++;
            roomUsage.TimesBookedSinceMaintenance++;

            roomUsage.GeneralWear += CalculateWear(stayDuration, numberOfGuests);

            if (roomUsage.TimesBookedSinceMaintenance >= 3)
            {
                roomUsage.CleaningState = CleaningState.NeedsCleaning;
            }
            else if (roomUsage.GeneralWear >= 0.5)
            {
                roomUsage.CleaningState = CleaningState.NeedsCleaning;
            }

            await _context.SaveChangesAsync();
        }

        private double CalculateWear(int stayDuration, int numberOfGuests)
        {
            return stayDuration * numberOfGuests * 0.01;
        }

        public async Task UpdateRoomUsageAfterCleaning(int roomId)
        {
            var roomUsage = await _context.RoomUsages.FirstOrDefaultAsync(ru => ru.RoomID == roomId);
            if (roomUsage == null)
            {
                throw new InvalidOperationException("Room usage record not found.");
            }

            roomUsage.CleaningState = CleaningState.Clean;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomUsageAfterMaintenance(int roomId)
        {
            var roomUsage = await _context.RoomUsages.FirstOrDefaultAsync(ru => ru.RoomID == roomId);
            if (roomUsage == null)
            {
                throw new InvalidOperationException("Room usage record not found.");
            }

            roomUsage.TimesBookedSinceMaintenance = 0;
            roomUsage.GeneralWear *= 0.5; 
            roomUsage.CleaningState = CleaningState.Clean;

            await _context.SaveChangesAsync();
        }

        public async Task PeriodicRoomUsageUpdate()
        {
            var roomUsages = await _context.RoomUsages.ToListAsync();
            foreach (var roomUsage in roomUsages)
            {
                // Example: Increase wear slightly over time
                roomUsage.GeneralWear = Math.Round(roomUsage.GeneralWear + 0.01, 2);

                // Check if maintenance or cleaning is needed
                if (roomUsage.GeneralWear >= BookingConstants.MaintenanceThreshold)
                {
                    roomUsage.CleaningState = CleaningState.NeedsCleaning;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}

