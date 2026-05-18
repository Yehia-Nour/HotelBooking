namespace HotelBooking.Application.Features.HotelBooking.Commands.Handlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationWithUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateReservationCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result<int>> Handle(CreateReservationWithUserCommand request, CancellationToken cancellationToken)
        {
            var req = request.Command;
            var roomIds = req.RoomIds.Distinct().ToList();

            var checkIn = req.CheckInDate!.Value.Date;
            var checkOut = req.CheckOutDate!.Value.Date;

            int nights = (checkOut - checkIn).Days;
            if (nights <= 0)
                return Error.Failure("Booking.InvalidDates", "Invalid date range.");


            var roomRepo = _unitOfWork.GetRepository<Room>();

            var existingRooms = await roomRepo.GetAllAsync(
              [HotelBookingRoomCriteriaSpecification.ByIds(roomIds)]);

            var existingIds = existingRooms.Select(r => r.Id).ToHashSet();
            var notFound = roomIds.Where(id => !existingIds.Contains(id)).ToList();
            if (notFound.Count > 0)
                return Error.Failure("Room.NotFound", $"Rooms not found: {string.Join(",", notFound)}");

            var availableSpec = HotelBookingRoomCriteriaSpecification.AvailableByIdsWithinDates(roomIds, checkIn, checkOut);

            var availableRooms = await roomRepo.GetAllAsync([availableSpec]);

            var availableIds = availableRooms.Select(r => r.Id).ToHashSet();
            var notAvailable = roomIds.Where(id => !availableIds.Contains(id)).ToList();
            if (notAvailable.Count > 0)
                return Error.Failure("Room.NotAvailable", $"Rooms not available for selected dates: {string.Join(",", notAvailable)}");

            Money amount = Money.Zero;

            foreach (var room in availableRooms)
            {
                var pricePerNight = new Money(room.Price);
                amount += pricePerNight * nights;
            }

            Money gst = TaxCalculator.CalculateGst(amount);
            Money totalAmount = TaxCalculator.CalculateTotal(amount);

            var reservationRepo = _unitOfWork.GetRepository<Reservation>();
            var reservationRoomRepo = _unitOfWork.GetRepository<ReservationRoom>();

            var reservation = new Reservation
            {
                UserID = request.UserId,
                BookingDate = DateTime.UtcNow,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                NumberOfNights = nights,
                TotalCost = totalAmount.Value,
                Status = ReservationStatus.Reserved,
            };

            await reservationRepo.AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();

            foreach (var room in availableRooms)
            {
                await reservationRoomRepo.AddAsync(new ReservationRoom
                {
                    ReservationID = reservation.Id,
                    RoomID = room.Id,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut
                });

                room.Status = BookingStatus.Occupied;
                roomRepo.Update(room);
            }

            await _unitOfWork.SaveChangesAsync();

            return reservation.Id;
        }
    }
}