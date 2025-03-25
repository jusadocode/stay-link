using stay_link.Server.DTO;
using stay_link.Server.Models;

namespace stay_link.Server.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDTO>> GetRooms();
        Task<RoomDTO?> GetRoom(int id);
        Task<Room> CreateRoom(CreateRoomDTO room);
        Task<bool> UpdateRoom(int id, UpdateRoomDTO updatedRoom);
        Task<bool> DeleteRoom(int id);
        Task<bool> RoomExists(int id);
    }
}