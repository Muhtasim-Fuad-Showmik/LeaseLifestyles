using System;
using System.ComponentModel.DataAnnotations;

namespace RentService.DTOs;

public class CreateRentDto
{
    [Required]
    public string Address { get; set; }
    [Required]
    public int FloorNumber { get; set; }
    [Required]
    public int Beds { get; set; }
    [Required]
    public int Baths { get; set; }
    [Required]
    public int Balconies { get; set; }
    [Required]
    public int HouseSize { get; set; }
    [Required]
    public string HouseSizeUnit { get; set; }
    [Required]
    public int LandSize { get; set; }
    [Required]
    public string LandSizeUnit { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime AvailableFrom { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    [Required]
    public int ReservedPrice { get; set; }
    [Required]
    public DateTime ConfirmBy { get; set; }
}
