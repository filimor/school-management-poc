using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Poc.Data;
using SchoolManagement.Poc.Models;

namespace SchoolManagement.Poc.Controllers;

[ApiController]
[Route("[Controller]")]
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class AddressesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AddressesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtém todos os endereços cadastrados.
    /// </summary>
    /// <remarks>Retorna uma lista com todos os endereços cadastrados.</remarks>
    /// <returns>Um array com os endereços.</returns>
    [HttpGet]
    public IActionResult GetAllAddresses()
    {
        var addresses = _context?.Addresses?.ToList();
        return Ok(addresses);
    }

    /// <summary>
    /// Obtém os dados de um endereço por id.
    /// </summary>
    /// <remarks>Retorna os dados de um endereço com base no id especificado.</remarks>
    /// <param name="id">Id do endereço.</param>
    [HttpGet("{id}")]
    public IActionResult GetAddressById(int id)
    {
        var address = _context?.Addresses?.FirstOrDefault(address => address.Id == id);

        if (address == null) return NotFound();

        var addressDto = _mapper.Map<AddressDto>(address);
        return Ok(addressDto);
    }

    /// <summary> Cadastra um endereço.</summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /addresses
    ///     {
    ///         "street": "R. Pamplona, 123",
    ///         "district": "Jardim Paulista",
    ///         "city": "São Paulo",
    ///         "state": "SP",
    ///         "country": "Brasil",
    ///         "zipCode": "01234-567"
    ///     }
    /// </remarks>
    /// <returns>O endereço cadastrado.</returns>
    /// <remarks>Retorna o endereço cadastrado.</remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateAddress([FromBody] AddressDto addressDto)
    {
        var address = _mapper.Map<Address>(addressDto);
        _context?.Addresses?.Add(address);
        _context?.SaveChanges();

        return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address);
    }

    /// <summary>
    /// Atualiza um endereço.
    /// </summary>
    /// <remarks>Atualiza o endereço correspondente ao ID recebido como parâmetro utilizando
    /// os dados recebidos no corpo da requisição.</remarks>
    /// <param name="id">Id do endereço a ser alterado.</param>
    [HttpPut("{id}")]
    public IActionResult UpdateAddress(int id, [FromBody] AddressDto addressDto)
    {
        var address = _context?.Addresses?.FirstOrDefault(address => address.Id == id);

        if (address == null) return NotFound();

        _mapper.Map(addressDto, address);
        _context?.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Exclui um endereço.
    /// </summary>
    /// <remarks>Exclui o endereço correspondente ao ID recebido.</remarks>
    /// <param name="id">Id do endereço a ser excluído.</param>
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