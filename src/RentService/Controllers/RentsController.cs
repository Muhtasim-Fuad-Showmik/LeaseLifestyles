using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentService.Data;
using RentService.DTOs;
using RentService.Entities;

namespace RentService.Controllers;

[ApiController]
[Route("api/rents")]
public class RentsController : ControllerBase
{
    private readonly RentDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public RentsController(RentDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<ActionResult<List<RentDto>>> GetAllRents(string date)
    {
        // Prepare a queryable object to get all Rents ordered by 
        // their addresses
        var query = _context.Rents.OrderByDescending(x => x.Item.Address).AsQueryable();

        // If a date is provided, filter the result based on Rents that have been updated after that date
        if (!string.IsNullOrEmpty(date))
        {
            query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
        }

        // Use the queryable object to get all of the rents mapped to RentDto
        // structure as a list
        return await query.ProjectTo<RentDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RentDto>> GetRentById(Guid id)
    {
        // Retrieve rent specified by id, including its associated Item
        var rent = await _context.Rents
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        // Return a 404 Not Found response if rent is not found
        if (rent == null) return NotFound();

        // Return the retrieved rent mapped according to the RentDto
        return _mapper.Map<RentDto>(rent);
    }

    [HttpPost]
    public async Task<ActionResult<RentDto>> CreateRent(CreateRentDto rentDto)
    {
        // Prepare AutoMapper from rentDto to rent
        var rent = _mapper.Map<Rent>(rentDto);

        // TODO: Add current user as creator
        rent.CreatedBy = "Fuad";

        // Save rent to be created to memory
        _context.Rents.Add(rent);

        // Map the new created rent to RentDto
        // and publish it to the Event Bus
        var newRent = _mapper.Map<RentDto>(rent);
        await _publishEndpoint.Publish(_mapper.Map<RentCreated>(newRent));

        // Save changes from memory to the database
        var result = await _context.SaveChangesAsync();

        // If no changes were applied, the request has failed
        if (result == 0) return BadRequest("Could not save changes to the DB");

        // Return reference to GetRentById to assist in finding the created data from the database
        return CreatedAtAction(nameof(GetRentById), new { rent.Id }, newRent);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRent(Guid id, UpdateRentDto updateRentDto)
    {
        // Retrieve the rent that is to be updated
        var rent = await _context.Rents.Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        // Return a 404 Not Found response if rent is not found
        if (rent == null) return NotFound();

        // TODO: Check if landlord == username

        // Update each field if an update for that field has been provided
        rent.Item.Address = updateRentDto.Address ?? rent.Item.Address;
        rent.Item.FloorNumber = updateRentDto.FloorNumber ?? rent.Item.FloorNumber;
        rent.Item.Beds = updateRentDto.Beds ?? rent.Item.Beds;
        rent.Item.Baths = updateRentDto.Baths ?? rent.Item.Baths;
        rent.Item.Balconies = updateRentDto.Balconies ?? rent.Item.Balconies;
        rent.Item.HouseSize = updateRentDto.HouseSize ?? rent.Item.HouseSize;
        rent.Item.HouseSizeUnit = updateRentDto.HouseSizeUnit ?? rent.Item.HouseSizeUnit;
        rent.Item.LandSize = updateRentDto.LandSize ?? rent.Item.LandSize;
        rent.Item.LandSizeUnit = updateRentDto.LandSizeUnit ?? rent.Item.LandSizeUnit;
        rent.Item.Description = updateRentDto.Description ?? rent.Item.Description;
        rent.Item.AvailableFrom = updateRentDto.AvailableFrom ?? rent.Item.AvailableFrom;

        // Publish the updated rent
        await _publishEndpoint.Publish(_mapper.Map<RentUpdated>(rent));

        // Save local changes to the database and retrieve the result
        var result = await _context.SaveChangesAsync() > 0;

        // If result is successful, return a Status 200 OK response
        if (result) return Ok();

        // If result is not successful, return a Status 400 Bad Request response
        return BadRequest("Problem saving changes");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRent(Guid id)
    {
        // Retrieve the rent that is to be deleted
        var rent = await _context.Rents.FindAsync(id);

        // Return a 404 Not Found response if rent is not found
        if (rent == null) return NotFound();

        // TODO: Check if landlord == username

        // Remove the rent from the database
        _context.Rents.Remove(rent);

        // Publish the deleted rent
        await _publishEndpoint.Publish<RentDeleted>(new { Id = rent.Id.ToString() });

        // Save local changes to the database and retrieve the result
        var result = await _context.SaveChangesAsync() > 0;

        // If result is not successful, return a Status 400 Bad Request response
        if (!result) return BadRequest("Problem deleting rent");

        // If result is successful, return a Status 200 OK response
        return Ok();
    }
}