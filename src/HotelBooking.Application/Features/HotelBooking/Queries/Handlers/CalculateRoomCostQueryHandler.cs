public class CalculateRoomCostQueryHandler : IRequestHandler<CalculateRoomCostQuery, Result<RoomCostResultDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CalculateRoomCostQueryHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

    public async Task<Result<RoomCostResultDTO>> Handle(CalculateRoomCostQuery request, CancellationToken cancellationToken)
    {
        var req = request.Request;

        int nights = (req.CheckOutDate!.Value.Date - req.CheckInDate!.Value.Date).Days;
        if (nights <= 0)
            return Error.Failure("Booking.InvalidDates", "Invalid date range.");

        var repo = _unitOfWork.GetRepository<Room>();

        var requestedIds = req.RoomIds.Distinct().ToList();

        var existingRooms = await repo.GetAllAsync([HotelBookingRoomCriteriaSpecification.ByIds(requestedIds)]);
        var existingIds = existingRooms.Select(r => r.Id).ToHashSet();

        var notFound = requestedIds.Where(id => !existingIds.Contains(id)).ToList();
        if (notFound.Count > 0)
            return Error.Failure("Room.NotFound", $"Rooms not found: {string.Join(",", notFound)}");

        var availableSpec = HotelBookingRoomCriteriaSpecification.AvailableByIdsWithinDates(
            requestedIds,
            req.CheckInDate!.Value.Date,
            req.CheckOutDate!.Value.Date);

        var availableRooms = await repo.GetAllAsync([availableSpec]);
        var availableIds = availableRooms.Select(r => r.Id).ToHashSet();

        var notAvailable = requestedIds.Where(id => !availableIds.Contains(id)).ToList();
        if (notAvailable.Count > 0)
            return Error.Failure("Room.NotAvailable", $"Rooms not available for selected dates: {string.Join(",", notAvailable)}");

        Money amount = Money.Zero;
        var breakdown = new List<RoomCostBreakdownDTO>(availableRooms.Count());

        foreach (var room in availableRooms)
        {
            var roomPrice = new Money(room.Price);
            var total = roomPrice * nights;

            amount += total;

            breakdown.Add(new RoomCostBreakdownDTO
            {
                RoomId = room.Id,
                RoomNumber = room.RoomNumber,
                PricePerNight = room.Price,
                TotalPrice = total.Value
            });
        }

        var gst = TaxCalculator.CalculateGst(amount);
        var totalAmount = TaxCalculator.CalculateTotal(amount);

        return new RoomCostResultDTO
        {
            NumberOfNights = nights,
            Amount = amount.Value,
            GST = gst.Value,
            TotalAmount = totalAmount.Value,
            Rooms = breakdown
        };
    }
}