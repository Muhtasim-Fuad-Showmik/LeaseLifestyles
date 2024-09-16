using System;

namespace RentService.Entities;

public class Rent
{
    public Guid Id { get; set; }
    public int ReservedPrice { get; set; } = 0;
    public string Landlord { get; set; }
    public string LandlordContactNo { get; set; }
    public string Tennant { get; set; }
    public int? RentAmount { get; set; }
    public int? CurrentHighBid { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ConfirmBy { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public Status Status { get; set; }
    public Item Item { get; set; }
}
