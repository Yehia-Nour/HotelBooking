namespace HotelBooking.Application.DTOs.FeedbackDTOs
{
    public record FeedbackDTO
    {
        public int Id { get; init; }
        public int Rating { get; init; }
        public string? Comment { get; init; }
        public DateTime FeedbackDate { get; init; }
        public int ReservationID { get; init; }
        public int GuestID { get; init; }
    }
}
