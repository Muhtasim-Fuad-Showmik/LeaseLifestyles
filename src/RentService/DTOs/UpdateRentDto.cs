using System;

namespace RentService.DTOs;

public class UpdateRentDto
{
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
}
