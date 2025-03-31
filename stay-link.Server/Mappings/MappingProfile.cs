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
            CreateMap<CreateBookingDTO, Booking>();
            CreateMap<Booking, BookingDTO>();
            CreateMap<BookingDTO, Booking>();
            CreateMap<Room, RoomDTO>();
            CreateMap<CreateRoomDTO, Room>();
            CreateMap<RoomDTO, Room>();
            CreateMap<UpdateRoomDTO, Room>();
            CreateMap<RoomFeatureDTO, RoomFeature>();
            CreateMap<RoomFeature, RoomFeatureDTO>();
            CreateMap<RoomFeature, RoomFeatureDetailsDTO>()
                .ForMember(dest => dest.BookingCount,
                    opt => opt.MapFrom(src => src.Bookings != null ? src.Bookings.Count : 0))
                .ForMember(dest => dest.RoomCount,
                    opt => opt.MapFrom(src => src.Rooms != null ? src.Rooms.Count : 0));
        }
    }
}
