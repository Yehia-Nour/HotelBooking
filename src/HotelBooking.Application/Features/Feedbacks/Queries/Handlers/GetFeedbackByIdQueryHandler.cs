using AutoMapper;
using HotelBooking.Application.DTOs.FeedbackDTOs;
using HotelBooking.Application.Features.Feedbacks.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.FeedbackSpecifications;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.Feedbacks.Queries.Handlers
{
    public class GetFeedbackByIdQueryHandler : IRequestHandler<GetFeedbackByIdQuery, Result<FeedbackDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFeedbackByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<FeedbackDTO>> Handle(GetFeedbackByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Feedback>();

            var feedback = await repo.GetAsync([FeedbackCriteriaSpecification.ById(request.FeedbackId)]);

            if (feedback is null)
                return Error.Failure("Feedback.NotFound", "Feedback not found.");

            return _mapper.Map<FeedbackDTO>(feedback);
        }
    }
}
