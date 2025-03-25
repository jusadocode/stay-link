using AutoMapper;
using stay_link.Server.DTO;
using stay_link.Server.Models;
using System.Diagnostics.CodeAnalysis;

namespace stay_link.Server.Mappings
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomDTO>();

            CreateMap<CreateRoomDTO, Room>();

            CreateMap<RoomDTO, Room>();

            CreateMap<UpdateRoomDTO, Room>();
        }
    }
}
