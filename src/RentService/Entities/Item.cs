using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentService.Entities;

[Table("Items")]
public class Item
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public int? FloorNumber { get; set; }
    public int Beds { get; set; }
    public int Baths { get; set; }
    public int? Balconies { get; set; }
    public int HouseSize { get; set; }
    public Unit HouseSizeUnit { get; set; }
    public int? LandSize { get; set; }
    public Unit? LandSizeUnit { get; set; }
    public string Description { get; set; }
    public DateTime AvailableFrom { get; set; } = DateTime.UtcNow;
    public string ImageUrl { get; set; }

    public Rent Rent { get; set; }
    public Guid RentId { get; set; }
}
