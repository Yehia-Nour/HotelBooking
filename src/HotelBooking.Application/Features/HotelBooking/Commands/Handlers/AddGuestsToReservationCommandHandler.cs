using HotelBooking.Application.Features.HotelBooking.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.GeographySpecifications;
using HotelBooking.Application.Specifications.HotelBookingSpecifications;
using HotelBooking.Domain.Entities.Geography;
using HotelBooking.Domain.Entities.Guests;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.HotelBooking.Commands.Handlers
{
    public class AddGuestsToReservationCommandHandler : IRequestHandler<AddGuestsToReservationWithUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddGuestsToReservationCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result> Handle(AddGuestsToReservationWithUserCommand request, CancellationToken cancellationToken)
        {
            var req = request.Command;
            var guestItems = req.Guests;

            var reservationRepo = _unitOfWork.GetRepository<Reservation>();

            var reservationCriteria = HotelBookingReservationCriteriaSpecification.ById(req.ReservationId);
            var reservationInclude = HotelBookingReservationIncludeSpecification.ReservationRooms();
            var reservation = await reservationRepo.GetAsync([reservationCriteria, reservationInclude]);

            if (reservation is null)
                return Result.Fail(Error.Failure("Reservation.NotFound", "Reservation does not exist."));

            if (reservation.UserID != request.UserId)
                return Result.Fail(Error.Failure("Reservation.Forbidden", "Reservation does not belong to the current user."));

            var roomIdToReservationRoomId = reservation.ReservationRooms
                .ToDictionary(x => x.RoomID, x => x.Id);

            var invalidRoomIds = guestItems
                .Select(g => g.RoomId)
                .Distinct()
                .Where(roomId => !roomIdToReservationRoomId.ContainsKey(roomId))
                .ToList();
            if (invalidRoomIds.Count > 0)
                return Result.Fail(Error.Failure("ReservationRoom.InvalidRoom", $"One or more RoomIDs are not valid for this reservation: {string.Join(",", invalidRoomIds)}"));

            var countryIds = guestItems.Select(g => g.CountryId).Distinct().ToList();
            var stateIds = guestItems.Select(g => g.StateId).Distinct().ToList();

            var countryRepo = _unitOfWork.GetRepository<Country>();
            var stateRepo = _unitOfWork.GetRepository<State>();

            var countries = await countryRepo.GetAllAsync([HotelBookingCountryCriteriaSpecification.ActiveByIds(countryIds)]);

            var states = await stateRepo.GetAllAsync([HotelBookingStateCriteriaSpecification.ActiveByIds(stateIds)]);

            var existingCountryIds = countries.Select(c => c.Id).ToHashSet();
            var invalidCountries = countryIds.Where(id => !existingCountryIds.Contains(id)).ToList();
            if (invalidCountries.Count > 0)
                return Result.Fail(Error.Failure("Geography.Country.NotFound", $"Invalid or inactive CountryIds: {string.Join(",", invalidCountries)}"));

            var existingStateIds = states.Select(s => s.Id).ToHashSet();
            var invalidStates = stateIds.Where(id => !existingStateIds.Contains(id)).ToList();
            if (invalidStates.Count > 0)
                return Result.Fail(Error.Failure("Geography.State.NotFound", $"Invalid or inactive StateIds: {string.Join(",", invalidStates)}"));

            var stateIdToCountryId = states.ToDictionary(s => s.Id, s => s.CountryID);

            var invalidMappings = guestItems
                .Where(g => stateIdToCountryId[g.StateId] != g.CountryId)
                .Select(g => $"StateId:{g.StateId}-CountryId:{g.CountryId}")
                .Distinct()
                .ToList();

            if (invalidMappings.Count > 0)
                return Result.Fail(Error.Failure("Geography.State.CountryMismatch", $"State does not belong to the specified Country: {string.Join(", ", invalidMappings)}"));


            var guestRepo = _unitOfWork.GetRepository<Guest>();
            var reservationGuestRepo = _unitOfWork.GetRepository<ReservationGuest>();

            var guests = guestItems.Select(g => new Guest
            {
                UserID = request.UserId,
                FirstName = g.FirstName.Trim(),
                LastName = g.LastName.Trim(),
                Email = g.Email.Trim(),
                Phone = g.Phone.Trim(),
                AgeGroup = g.AgeGroup,
                Address = g.Address.Trim(),
                CountryID = g.CountryId,
                StateID = g.StateId,
                CreatedBy = request.UserEmail,
                CreatedDate = DateTime.UtcNow
            }).ToList();

            await guestRepo.AddRangeAsync(guests);
            await _unitOfWork.SaveChangesAsync();

            for (int i = 0; i < guests.Count; i++)
            {
                var input = guestItems[i];

                await reservationGuestRepo.AddAsync(new ReservationGuest
                {
                    ReservationRoomID = roomIdToReservationRoomId[input.RoomId],
                    GuestID = guests[i].Id
                });
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }
    }
}