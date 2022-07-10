using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Poc.Data;
using SchoolManagement.Poc.Models;

namespace SchoolManagement.Poc.Controllers;

[ApiController]
[Route("[Controller]")]
public class AddressesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AddressesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllAddresses()
    {
        var addresses = _context?.Addresses?.ToList();
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    public IActionResult GetAddressById(int id)
    {
        var address = _context?.Addresses?.FirstOrDefault(address => address.Id == id);

        if (address == null) return NotFound();

        var addressDto = _mapper.Map<AddressDto>(address);
        return Ok(addressDto);
    }

    [HttpPost]
    public IActionResult CreateAddress([FromBody] AddressDto addressDto)
    {
        var address = _mapper.Map<Address>(addressDto);
        _context?.Addresses?.Add(address);
        _context?.SaveChanges();

        return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAddress(int id, [FromBody] AddressDto addressDto)
    {
        var address = _context?.Addresses?.FirstOrDefault(address => address.Id == id);

        if (address == null) return NotFound();

        _mapper.Map(addressDto, address);
        _context?.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAddress(int id)
    {
        var address = _context?.Addresses?.FirstOrDefault(address => address.Id == id);

        if (address == null) return NotFound();

        _context?.Addresses?.Remove(address);
        _context?.SaveChanges();

        return NoContent();
    }
}