using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentService.Data;
using RentService.DTOs;

namespace RentService.Controllers;

[ApiController]
[Route("api/rents")]
public class RentsController : ControllerBase
{
    private readonly RentDbContext _context;
    private readonly IMapper _mapper;

    public RentsController(RentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<RentDto>>> GetAllRents()
    {
        /* Retrieve all rents, including the associated item,
        ordered by their house sizes */
        var rents = await _context.Rents
            .Include(x => x.Item)
            .OrderBy(x => x.Item.HouseSize)
            .ToListAsync();

        // Return the retrieved rents mapped according to the RentDto
        return _mapper.Map<List<RentDto>>(rents);
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
}
