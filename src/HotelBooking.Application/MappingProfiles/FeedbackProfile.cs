using AutoMapper;
using HotelBooking.Application.DTOs.FeedbackDTOs;
using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Application.MappingProfiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<Feedback, FeedbackDTO>();
        }
    }
}
