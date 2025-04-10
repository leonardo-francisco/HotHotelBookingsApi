using AutoMapper;
using HHB.Application.DTO;
using HHB.Domain.Entities;
using HHB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HHB.Application.Mapper
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            // AdditionalService
            CreateMap<AdditionalService, AdditionalServiceDto>()               
                .ReverseMap();

            // Booking
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.AdditionalService, opt => opt.MapFrom(src => src.AdditionalService))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<BookingStatus>(src.Status)))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => Enum.Parse<PaymentStatus>(src.PaymentStatus)))
                .ForMember(dest => dest.AdditionalService, opt => opt.MapFrom(src => src.AdditionalService));

            // Customer
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Bookings, opt => opt.MapFrom(src => src.Bookings))
                .ReverseMap();

            // Customer Review
            CreateMap<CustomerReview, CustomerReviewDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            // Hotel
            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.FoundedYear, opt => opt.MapFrom(src => src.FoundedYear))
                .ForMember(dest => dest.ClosedYear, opt => opt.MapFrom(src => src.ClosedYear))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms))
                .ReverseMap();

            // Address
            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();

            // Room
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType.ToString()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
                .ForMember(dest => dest.PricePerNight, opt => opt.MapFrom(src => src.PricePerNight))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
                .ReverseMap()
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src =>
                     Enum.Parse<RoomType>(src.RoomType)
                 ));
        }
    }
}
