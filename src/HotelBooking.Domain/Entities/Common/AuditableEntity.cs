namespace HotelBooking.Domain.Entities.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = default!;
        public DateTime ModifiedDate { get; set; }
    }

}
