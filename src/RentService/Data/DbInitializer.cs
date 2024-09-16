using System;
using Microsoft.EntityFrameworkCore;
using RentService.Entities;

namespace RentService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        /**
         * Create scope for app services so that it will get 
         * disposed off after finished usage within this scope
         **/
        using var scope = app.Services.CreateScope();

        // See data using the Rent Db Context
        SeedData(scope.ServiceProvider.GetService<RentDbContext>());
    }

    private static void SeedData(RentDbContext context)
    {
        // Apply any migrations not yet applied to the database
        context.Database.Migrate();

        // Do nothing in this method if any data is already present for rents
        if (context.Rents.Any())
        {
            Console.WriteLine("Already have data - no need to seed");
            return;
        }

        // Create rents to be added as seed to the database
        // P.S: Data generated using Codeium
        var rents = new List<Rent>()
        {
            // Lalmatia, Block B, 1800 Sqft
            new Rent
            {
                Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
                Status = Status.Live,
                ReservedPrice = 45000,
                Landlord = "Wade",
                LandlordContactNo = "1234567890",
                Tennant = "Logan",
                ConfirmBy = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Address = "Lalmatia, Block B",
                    Beds = 3,
                    Baths = 4,
                    Balconies = 3,
                    HouseSize = 1800,
                    HouseSizeUnit = Unit.Sqft,
                    LandSize = 4000,
                    LandSizeUnit = Unit.Katha,
                    AvailableFrom = DateTime.UtcNow.AddDays(30),
                    Description = "A beautiful, well ventilated and open flat is ready to move in the amicable locality of Lalmatia. To make sure you are gratified with the home that has always been pictured in your mind, we have enlisted the image of this flat below. Utilities are readily available in this apartment. There are balconies to make your apartment life more comfortable in the flat.\n\nMake yourself a happy buyer by calling us about this beautiful apartment right away!",
                    ImageUrl = "https://images.unsplash.com/photo-1492659991124-ba70990ba2f4?q=80&w=1922&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
            // Jatra Bari Thana, 700Sqft
            new Rent
            {
                Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                Status = Status.Finished,
                ReservedPrice = 8500,
                Landlord = "Bruce",
                LandlordContactNo = "01723456789",
                Tennant = "Clark",
                RentAmount = 9000,
                CurrentHighBid = 9000,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow.AddDays(-5),
                ConfirmBy = DateTime.UtcNow.AddDays(10),
                ConfirmedAt = DateTime.UtcNow.AddDays(-2),
                Item = new Item
                {
                    Address = "Bibir Bagicha, Jatra Bari",
                    Beds = 2,
                    Baths = 1,
                    Balconies = 2,
                    HouseSize = 700,
                    HouseSizeUnit = Unit.Sqft,
                    LandSize = 400,
                    LandSizeUnit = Unit.Katha,
                    AvailableFrom = DateTime.UtcNow.AddDays(15),
                    Description = "If you are looking for a home with 2 beds and 1 bath, this should be perfect and within an affordable range of BDT 8,500. Well fitted with gas, electricity and water supplies, this flat is ideal to move in for new inhabitants. The building welcomes with a very commodious parking lot and an elevator to take to the apartment to this apartment. After entering the flat, you will find ample rooms cited for your recessing time and also happy dine hour. The kitchen area is just close to the dining space which gives the impression of ample light and space to have a content cooking time. Well-fitted bathrooms with resilient fixtures as per your prerequisites. One allotted parking space would come along with this beautiful flat. Lots of restaurants, shopping places and schools are nearby as well as parks so the neighborhood is great as well. Advance for 2 months.\n\nDon’t miss this affordable offer and we are just a call away to close the deal for you!",
                    ImageUrl = "https://images.unsplash.com/photo-1516923427950-a696555ab884?q=80&w=2068&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
            // Uttara, Sector 12, 1200 Sqft
            new Rent
            {
                Id = Guid.Parse("3f4c4d7c-5f4d-4a7c-8efc-7b9f4a70b7a7"),
                Status = Status.Live,
                ReservedPrice = 35000,
                Landlord = "Diana",
                LandlordContactNo = "01987654321",
                Tennant = "Bruce",
                ConfirmBy = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Address = "Uttara, Sector 12, Plot 17",
                    Beds = 3,
                    Baths = 3,
                    Balconies = 2,
                    HouseSize = 1200,
                    HouseSizeUnit = Unit.Sqft,
                    LandSize = 2000,
                    LandSizeUnit = Unit.Katha,
                    AvailableFrom = DateTime.UtcNow.AddDays(45),
                    Description = "A beautiful, well ventilated and open flat is ready to move in the amicable locality of Uttara. To make sure you are gratified with the home that has always been pictured in your mind, we have enlisted the image of this flat below. Utilities are readily available in this apartment. There are balconies to make your apartment life more comfortable in the flat.\n\nMake yourself a happy buyer by calling us about this beautiful apartment right away!",
                    ImageUrl = "https://images.unsplash.com/photo-1534655610770-dd69616f05ff?q=80&w=1856&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
            // Baridhara DOHS, 1000 Sqft
            new Rent
            {
                Id = Guid.Parse("6f2a2c4e-0aeb-4b3f-9d4b-01cfae2c6b1f"),
                Status = Status.Live,
                ReservedPrice = 30000,
                Landlord = "Jean",
                LandlordContactNo = "01687654321",
                Tennant = "Diluc",
                ConfirmBy = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Address = "Baridhara DOHS, Sector 12, Plot 17",
                    Beds = 3,
                    Baths = 3,
                    Balconies = 2,
                    HouseSize = 1000,
                    HouseSizeUnit = Unit.Sqft,
                    LandSize = 2000,
                    LandSizeUnit = Unit.Katha,
                    AvailableFrom = DateTime.UtcNow.AddDays(45),
                    Description = "A beautiful, well ventilated and open flat is ready to move in the amicable locality of Uttara. To make sure you are gratified with the home that has always been pictured in your mind, we have enlisted the image of this flat below. Utilities are readily available in this apartment. There are balconies to make your apartment life more comfortable in the flat.\n\nMake yourself a happy buyer by calling us about this beautiful apartment right away!",
                    ImageUrl = "https://images.unsplash.com/photo-1515120263166-b676e1f61045?q=80&w=2069&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
            // DOHS, 1800 Sqft
            new Rent
            {
                Id = Guid.Parse("6f2a2c4e-0aeb-4b3f-9d4b-01cfae2c6b2f"),
                Status = Status.Live,
                ReservedPrice = 45000,
                Landlord = "John",
                LandlordContactNo = "01887654321",
                Tennant = "Trish",
                ConfirmBy = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Address = "DOHS, Sector 12, Plot 17",
                    Beds = 3,
                    Baths = 4,
                    Balconies = 3,
                    HouseSize = 1800,
                    HouseSizeUnit = Unit.Sqft,
                    LandSize = 2000,
                    LandSizeUnit = Unit.Katha,
                    AvailableFrom = DateTime.UtcNow.AddDays(45),
                    Description = "A beautiful, well ventilated and open flat is ready to move in the amicable locality of Uttara. To make sure you are gratified with the home that has always been pictured in your mind, we have enlisted the image of this flat below. Utilities are readily available in this apartment. There are balconies to make your apartment life more comfortable in the flat.\n\nMake yourself a happy buyer by calling us about this beautiful apartment right away!",
                    ImageUrl = "https://images.unsplash.com/photo-1525953776754-6c4b7ee655ab?q=80&w=1921&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
            // Gulshan 1, 1200 Sqft
            new Rent
            {
                Id = Guid.Parse("6f2a2c4e-0aeb-4b3f-9d4b-01cfae2c6b3f"),
                Status = Status.Live,
                ReservedPrice = 40000,
                Landlord = "Jane",
                LandlordContactNo = "01987654321",
                Tennant = "Peter",
                ConfirmBy = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Address = "Gulshan 1, Sector 12, Plot 17",
                    Beds = 3,
                    Baths = 3,
                    Balconies = 2,
                    HouseSize = 1200,
                    HouseSizeUnit = Unit.Sqft,
                    LandSize = 2000,
                    LandSizeUnit = Unit.Katha,
                    AvailableFrom = DateTime.UtcNow.AddDays(45),
                    Description = "A beautiful, well ventilated and open flat is ready to move in the amicable locality of Uttara. To make sure you are gratified with the home that has always been pictured in your mind, we have enlisted the image of this flat below. Utilities are readily available in this apartment. There are balconies to make your apartment life more comfortable in the flat.\n\nMake yourself a happy buyer by calling us about this beautiful apartment right away!",
                    ImageUrl = "https://plus.unsplash.com/premium_photo-1670275658703-33fb95fe50d8?q=80&w=1888&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
            // Bashundhara, 1800 Sqft
            new Rent
            {
                Id = Guid.Parse("7f5e8a7a-2b9c-4a7e-96b3-0a9c68b5e0a9"),
                Status = Status.Live,
                ReservedPrice = 45000,
                Landlord = "John",
                LandlordContactNo = "01987654322",
                Tennant = "Mithila",
                ConfirmBy = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Address = "Bashundhara, Block B",
                    Beds = 3,
                    Baths = 4,
                    Balconies = 3,
                    HouseSize = 1800,
                    HouseSizeUnit = Unit.Sqft,
                    LandSize = 4000,
                    LandSizeUnit = Unit.Katha,
                    AvailableFrom = DateTime.UtcNow.AddDays(45),
                    Description = "A beautiful, well ventilated and open flat is ready to move in the amicable locality of Uttara. To make sure you are gratified with the home that has always been pictured in your mind, we have enlisted the image of this flat below. Utilities are readily available in this apartment. There are balconies to make your apartment life more comfortable in the flat.\n\nMake yourself a happy buyer by calling us about this beautiful apartment right away!",
                    ImageUrl = "https://plus.unsplash.com/premium_photo-1680157072373-f180488fd8ae?q=80&w=1881&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
            // Dhanmondi, 2000 Sqft
            new Rent
            {
                Id = Guid.Parse("b1d1e38f-6b6e-4834-8f4e-2e3c0a8b6f7c"),
                Status = Status.Live,
                ReservedPrice = 50000,
                Landlord = "Emma",
                LandlordContactNo = "01987654323",
                Tennant = "Andrew",
                ConfirmBy = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Address = "Dhanmondi, Sector 12, Plot 17",
                    Beds = 3,
                    Baths = 4,
                    Balconies = 2,
                    HouseSize = 2000,
                    HouseSizeUnit = Unit.Sqft,
                    LandSize = 4000,
                    LandSizeUnit = Unit.Katha,
                    AvailableFrom = DateTime.UtcNow.AddDays(45),
                    Description = "A beautiful, well ventilated and open flat is ready to move in the amicable locality of Uttara. To make sure you are gratified with the home that has always been pictured in your mind, we have enlisted the image of this flat below. Utilities are readily available in this apartment. There are balconies to make your apartment life more comfortable in the flat.\n\nMake yourself a happy buyer by calling us about this beautiful apartment right away!",
                    ImageUrl = "https://images.unsplash.com/photo-1460408037948-b89a5e837b41?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
            // Mirpur, 1200 Sqft
            new Rent
            {
                Id = Guid.Parse("b1d1e38f-6b6e-4834-8f4e-2e3c0a8b6f7d"),
                Status = Status.Live,
                ReservedPrice = 35000,
                Landlord = "Olivia",
                LandlordContactNo = "01987654324",
                Tennant = "Oliver",
                ConfirmBy = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Address = "Mirpur, Sector 12, Plot 17",
                    Beds = 3,
                    Baths = 3,
                    Balconies = 2,
                    HouseSize = 1200,
                    HouseSizeUnit = Unit.Sqft,
                    LandSize = 2000,
                    LandSizeUnit = Unit.Katha,
                    AvailableFrom = DateTime.UtcNow.AddDays(45),
                    Description = "A beautiful, well ventilated and open flat is ready to move in the amicable locality of Uttara. To make sure you are gratified with the home that has always been pictured in your mind, we have enlisted the image of this flat below. Utilities are readily available in this apartment. There are balconies to make your apartment life more comfortable in the flat.\n\nMake yourself a happy buyer by calling us about this beautiful apartment right away!",
                    ImageUrl = "https://images.unsplash.com/photo-1489936724440-afaf5a115ede?q=80&w=2051&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
            // Badda, 1500 Sqft
            new Rent
            {
                Id = Guid.Parse("b1d1e38f-6b6e-4834-8f4e-2e3c0a8b6f7e"),
                Status = Status.Live,
                ReservedPrice = 40000,
                Landlord = "William",
                LandlordContactNo = "01987654325",
                Tennant = "Dolores",
                ConfirmBy = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Address = "Badda, Sector 12, Plot 17",
                    Beds = 3,
                    Baths = 4,
                    Balconies = 3,
                    HouseSize = 1500,
                    HouseSizeUnit = Unit.Sqft,
                    LandSize = 3000,
                    LandSizeUnit = Unit.Katha,
                    AvailableFrom = DateTime.UtcNow.AddDays(45),
                    Description = "A beautiful, well ventilated and open flat is ready to move in the amicable locality of Uttara. To make sure you are gratified with the home that has always been pictured in your mind, we have enlisted the image of this flat below. Utilities are readily available in this apartment. There are balconies to make your apartment life more comfortable in the flat.\n\nMake yourself a happy buyer by calling us about this beautiful apartment right away!",
                    ImageUrl = "https://images.unsplash.com/photo-1512621480870-77463b1b90c7?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
        };

        // Add rents to the memory
        context.AddRange(rents);

        // Update database to add rents from the memory to the database
        context.SaveChanges();
    }
}
