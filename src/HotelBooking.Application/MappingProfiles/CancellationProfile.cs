using AutoMapper;
using HotelBooking.Application.DTOs.CancellationDTOs;
using HotelBooking.Application.Features.CancellationPolicies.Commands.Requests;
using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Application.MappingProfiles
{
    public class CancellationProfile : Profile
    {
        public CancellationProfile()
        {
            CreateMap<CancellationPolicy, CancellationPolicyDTO>();

            CreateMap<CancellationRequest, CancellationRequestListItemDTO>();

            CreateMap<CreateCancellationPolicyCommand, CancellationPolicy>();

            CreateMap<UpdateCancellationPolicyCommand, CancellationPolicy>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember is not null));
        }
    }
}
