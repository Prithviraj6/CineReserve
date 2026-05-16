using AutoMapper;
using CineReserve.Application.DTOs.Booking;
using CineReserve.Application.DTOs.Movie;
using CineReserve.Application.DTOs.Seat;
using CineReserve.Application.DTOs.ShowTime;
using CineReserve.Application.DTOs.User;
using CineReserve.Domain.Entities;

namespace CineReserve.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Movie
            CreateMap<Movie, MovieDto>();
            CreateMap<CreateMovieDto, Movie>();

            // Showtime
            CreateMap<Showtime, ShowTimeDto>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.TheaterHallName, opt => opt.MapFrom(src => src.TheaterHall.Name));
            CreateMap<CreateShowTimeDto, Showtime>();

            // Booking
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Showtime.Movie.Title))
                .ForMember(dest => dest.TheaterHallName, opt => opt.MapFrom(src => src.Showtime.TheaterHall.Name))
                .ForMember(dest => dest.ShowTimeUtc, opt => opt.MapFrom(src => src.Showtime.StartTimeUtc))
                .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.TicketDetails));

            // TicketDetail
            CreateMap<TicketDetail, TicketDetailDto>()
                .ForMember(dest => dest.RowLabel, opt => opt.MapFrom(src => src.Seat.RowLabel))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.SeatNumber))
                .ForMember(dest => dest.SeatType, opt => opt.MapFrom(src => src.Seat.SeatType.ToString()))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.SeatPrice));

            // User
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
        }
    }
}
