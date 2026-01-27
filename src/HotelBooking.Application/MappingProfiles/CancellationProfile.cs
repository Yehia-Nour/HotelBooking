using AutoMapper;
using HotelBooking.Application.DTOs.CancellationDTOs;
using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Application.MappingProfiles
{
    public class CancellationProfile : Profile
    {
        public CancellationProfile()
        {
            CreateMap<CancellationPolicy, CancellationPolicyDTO>();

            CreateMap<CancellationRequest, CancellationRequestListItemDTO>();

            //CreateMap<CreateCancellationPolicyCommand, CancellationPolicy>();
        }
    }
}
