using System;

namespace RentService.DTOs;

public class RentDto
{
    public Guid Id { get; set; }
    public int ReservedPrice { get; set; }
    public string Landlord { get; set; }
    public string LandlordContactNo { get; set; }
    public string Tennant { get; set; }
    public int RentAmount { get; set; }
    public int CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime ConfirmBy { get; set; }
    public DateTime ConfirmedAt { get; set; }
    public string Status { get; set; }
    public string Address { get; set; }
    public int Beds { get; set; }
    public int Baths { get; set; }
    public int Balconies { get; set; }
    public int HouseSize { get; set; }
    public string HouseSizeUnit { get; set; }
    public int LandSize { get; set; }
    public string LandSizeUnit { get; set; }
    public string Description { get; set; }
    public DateTime AvailableFrom { get; set; }
    public string ImageUrl { get; set; }
}
