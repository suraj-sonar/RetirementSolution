using Base.Application.ServiceContracts;
using Base.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Base.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PersonAddressController : ControllerBase
{
    private readonly IPersonAddressService _service;
    private readonly ILogger<PersonAddressController> _logger;

    public PersonAddressController(IPersonAddressService service, ILogger<PersonAddressController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all person addresses.");
        var addresses = await _service.GetAllAsync();
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation($"Getting person address with ID: {id}");
        var address = await _service.GetByIdAsync(id);
        return address == null ? NotFound() : Ok(address);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PersonAddress address)
    {
        _logger.LogInformation("Creating a new person address.");
        var id = await _service.AddAsync(address);
        return CreatedAtAction(nameof(Get), new { id }, address);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PersonAddress address)
    {
        _logger.LogInformation($"Updating person address with ID: {id}");
        address.id = id;
        var updated = await _service.UpdateAsync(address);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation($"Deleting person address with ID: {id}");
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}

