using AutoMapper;
using AutoMapper.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Data;
using stay_link.Server.DTO;
using stay_link.Server.Helpers;
using stay_link.Server.Models;
using System.Collections.Generic;

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


        public async Task<IEnumerable<int>> GetAvailableRoomIds(DateOnly checkIn, DateOnly checkOut)
        {
            var availableRoomIds = await _context.Rooms
              .Where(r => !r.Bookings.Any(b => b.CheckInDate < checkOut && checkIn < b.CheckOutDate))
              .Select(r => r.Id)  
              .ToListAsync();

            return availableRoomIds;
        }

        public async Task<IEnumerable<RoomOfferDTO>> GetRoomOffers(DateOnly checkIn, DateOnly checkOut, int guestCount, List<RoomFeature> preferences)
        {
            var roomGroups = await FindMatchingRoomGroupsPreference(checkIn, checkOut, guestCount, preferences);
            return _mapper.Map<IEnumerable<RoomOfferDTO>>(roomGroups);
        }

        public async Task<IEnumerable<RoomClosureDTO>> GetRoomClosures()
        {
            var closures = await _context.RoomClosure
              .ToListAsync();

            return _mapper.Map<IEnumerable<RoomClosureDTO>>(closures);
        }

        public async Task<RoomDTO?> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            return _mapper.Map<RoomDTO>(room);
        }

        public async Task<RoomDTO> CreateRoom(CreateRoomDTO createRoomDTO)
        {

            var features = await _context.RoomFeatures
                .Where(rf => createRoomDTO.FeatureIds.Contains(rf.Id))
                .ToListAsync();

            var room = _mapper.Map<Room>(createRoomDTO);

            room.Title = $"{room.Title}";

            room.Features = new List<RoomFeature>(features);

            _context.Rooms.Add(room);

            var roomUsage = new RoomUsage { RoomId = room.Id };

            _context.RoomUsages.AddRange(roomUsage);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoomDTO>(room);

        }

        public async Task<List<RoomDTO>> CreateRooms(CreateRoomDTO createRoomDTO, int amountOfRooms)
        {
            var createdRooms = new List<Room>();

            var features = await _context.RoomFeatures
                .Where(rf => createRoomDTO.FeatureIds.Contains(rf.Id))
                .ToListAsync();

            for (int index = 1; index <= amountOfRooms; index++)
            {
                var room = _mapper.Map<Room>(createRoomDTO);

                room.Title = $"{room.Title} {index}";

                room.Features = new List<RoomFeature>(features);

                _context.Rooms.Add(room);

                createdRooms.Add(room);
            }

            await _context.SaveChangesAsync();

            var roomUsages = createdRooms.Select(room => new RoomUsage { RoomId = room.Id }).ToList();

            _context.RoomUsages.AddRange(roomUsages);
            await _context.SaveChangesAsync();

            return createdRooms.Select(room => _mapper.Map<RoomDTO>(room)).ToList();
        }


        public async Task<bool> UpdateRoom(int id, UpdateRoomDTO roomDTO)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return false;

            room.RoomType = Enum.Parse<RoomType>(roomDTO.RoomType);

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
            return await _context.Rooms.AnyAsync(e => e.Id == id);
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

        public async Task<List<RoomUsageDTO>> GetRoomsUsages()
        {
            var usages = await _context.RoomUsages
                .ToListAsync();

            return _mapper.Map<List<RoomUsageDTO>>(usages);

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

            return roomScores.OrderByDescending(r => r.Value).Select(r => r.Key).Take(10).ToList();
        }

        public async Task<Dictionary<Room, double>> FindCandidateRoomScoresForGroups(DateOnly checkIn, DateOnly checkOut,  List<RoomFeature> preferences)
        {
            var availableRooms = await _context.Rooms
                .Where(r => !r.Bookings.Any(b => b.CheckInDate < checkOut && checkIn < b.CheckOutDate))
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


                double finalRoomScore = 0.6 * preferenceScore + 0.4 * maintenanceScore;

                Console.WriteLine(room.Title + ' ' + finalRoomScore);
                Console.WriteLine("Preference score: " + preferenceScore);
                Console.WriteLine("Maintenance score: " + maintenanceScore);

                roomScores[room] = finalRoomScore;

            }

            return roomScores;
        }

        public async Task<List<RoomOffer?>> FindMatchingRoomGroupsPreference(DateOnly checkIn, DateOnly checkOut, int guestCount, List<RoomFeature> preferences)
        {

            int MAX_ROOMS_TO_COMBINE = 3;

            var roomScores = await FindCandidateRoomScoresForGroups(checkIn, checkOut, preferences); 

            var availableRooms = roomScores.OrderByDescending(r => r.Value).Select(r => r.Key).ToList();

            //var wearHeuristicRooms = scoreHeuristicRooms.OrderByDescending(r => r.Value).Select(r => r.Key).Take(20);

            if (!availableRooms.Any())
                return null;

            var allCombinations = new List<List<Room>>();

            var singleRoomCombinations = availableRooms.Where(r => r.MaxOccupancy >= guestCount).Take(5);

            allCombinations.AddRange(singleRoomCombinations.Select(r => new List<Room> { r }));

            if (MAX_ROOMS_TO_COMBINE >= 2)
            {
                int pairCount = 0;
                for (int i = 0; i < availableRooms.Count; i++)
                {
                    for (int j = i + 1; j < availableRooms.Count; j++)
                    {
                        Room room1 = availableRooms[i];
                        Room room2 = availableRooms[j];
                        if (room1.MaxOccupancy + room2.MaxOccupancy >= guestCount)
                        {
                            allCombinations.Add(new List<Room> { room1, room2 });
                            pairCount++;
                        }
                    }
                }
            }

            if (MAX_ROOMS_TO_COMBINE >= 3)
            {
                var poolFor = availableRooms.Take(30).ToList();
                int tripletCount = 0;
                for (int i = 0; i < availableRooms.Count; i++)
                {
                    for (int j = i + 1; j < availableRooms.Count; j++)
                    {
                        for (int l = j + 1; l < availableRooms.Count; l++)
                        {
                            Room room1 = availableRooms[i];
                            Room room2 = availableRooms[j];
                            Room room3 = availableRooms[l];
                            if (room1.MaxOccupancy + room2.MaxOccupancy + room3.MaxOccupancy >= guestCount)
                            {
                                allCombinations.Add(new List<Room> { room1, room2, room3 });
                                tripletCount++;
                            }
                        }
                    }
                }
            }

            var rankedOptions = new List<RoomOffer>();
            foreach (var combo in allCombinations)
            {
                // Ensure combo is not null or empty before scoring
                if (combo != null && combo.Count > 0)
                {
                    double score = RoomCalculationUtils.CalculateCombinationScore(combo, preferences, guestCount, roomScores);
                    decimal totalPrice = combo.Sum(r => r.Price); // Assuming Room has a Price property
                    rankedOptions.Add(new RoomOffer { Rooms = combo, Score = score, TotalPrice = totalPrice });
                }
            }

            return rankedOptions
                .OrderByDescending(opt => opt.Score)
                .ThenBy(opt => opt.TotalPrice)
                .Take(10)
                .ToList();

        }

        public async Task<RoomClosureDTO> CreateRoomClosure(CreateRoomClosureDTO closureDto)
        {
            // Check if the room exists
            var room = await _context.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(r => r.Id == closureDto.RoomId);

            if (room == null)
            {
                throw new InvalidOperationException("Room not found.");
            }

            bool isBooked = room.Bookings.Any(b =>
                b.CheckInDate < closureDto.EndDate && closureDto.StartDate < b.CheckOutDate
            );

            if (isBooked)
            {
                throw new InvalidOperationException("Room is booked during the selected closure period.");
            }

            var closure = _mapper.Map<RoomClosure>(closureDto);

            _context.RoomClosure.Add(closure);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoomClosureDTO>(closure);
        }


        public async Task UpdateRoomUsageAfterBooking(int roomId, int numberOfGuests, int stayDuration)
        {
            var roomUsage = await _context.RoomUsages.FirstOrDefaultAsync(ru => ru.RoomId == roomId);
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
            var roomUsage = await _context.RoomUsages.FirstOrDefaultAsync(ru => ru.RoomId == roomId);
            if (roomUsage == null)
            {
                throw new InvalidOperationException("Room usage record not found.");
            }

            roomUsage.CleaningState = CleaningState.Clean;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomUsageAfterMaintenance(int roomId)
        {
            var roomUsage = await _context.RoomUsages.FirstOrDefaultAsync(ru => ru.RoomId == roomId);
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
                if (roomUsage.GeneralWear >= 0.4)
                {
                    roomUsage.CleaningState = CleaningState.NeedsCleaning;
                }
                else if (roomUsage.GeneralWear >= 0.8)
                {
                    roomUsage.CleaningState = CleaningState.DeepCleaning;
                }

                if(roomUsage.TimesBookedThisYear >= 20)
                {
                    roomUsage.CleaningState = CleaningState.DeepCleaning;
                }

            }

            await _context.SaveChangesAsync();
        }
    }
}

