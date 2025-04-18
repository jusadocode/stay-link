using AutoMapper;
using stay_link.Server.DTO.Bookings;
using stay_link.Server.DTO.RoomClosure;
using stay_link.Server.DTO.RoomOperations;
using stay_link.Server.DTO.Rooms;
using stay_link.Server.Models.Bookings;
using stay_link.Server.Models.RoomOperations;
using stay_link.Server.Models.Rooms;
using System.Diagnostics.CodeAnalysis;

namespace stay_link.Server.Mappings
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookingDTO, Booking>();
            CreateMap<Booking, BookingDTO>()
                .ForMember(dest => dest.RoomIds,
                    opt => opt.MapFrom(src => src.Rooms.Select(r => r.Id).ToList()));
            CreateMap<Room, RoomDTO>()
                .ForMember(dest => dest.RoomType,
                    opt => opt.MapFrom(src => src.RoomType.ToString()))
                .ForMember(dest => dest.GeneralWear,
                    opt => opt.MapFrom(src => src.RoomUsage != null ? src.RoomUsage.GeneralWear : 0));

            CreateMap<CreateRoomDTO, Room>();
            CreateMap<RoomDTO, Room>();
            CreateMap<RoomOffer, RoomOfferDTO>();
            CreateMap<UpdateRoomDTO, Room>();

            CreateMap<Room, RoomAvailabilityDTO>();

            CreateMap<RoomFeatureDTO, RoomFeature>();
            CreateMap<RoomFeature, RoomFeatureDTO>();
            CreateMap<RoomFeature, RoomFeatureDetailsDTO>()
                .ForMember(dest => dest.BookingCount,
                    opt => opt.MapFrom(src => src.Bookings != null ? src.Bookings.Count : 0))
                .ForMember(dest => dest.RoomCount,
                    opt => opt.MapFrom(src => src.Rooms != null ? src.Rooms.Count : 0));

            CreateMap<RoomUsage, RoomUsageDTO>()
            .ForMember(dest => dest.CleaningState,
                    opt => opt.MapFrom(src => src.CleaningState.ToString()));

            CreateMap<CreateRoomClosureDTO, RoomClosure>();
            CreateMap<RoomClosure, RoomClosureDTO>();
        }
    }
}
