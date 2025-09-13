using Base.Application.Logging;
using Base.Application.ServiceContracts;
using Base.Application.Validation;
using Base.Domain;
using Microsoft.AspNetCore.Mvc;
using Base.Application.Exceptions;

namespace Base.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        readonly IPersonService _personService;
        readonly IAppLogger<PersonController> _logger;
        public PersonController(IPersonService personService, IAppLogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("Getting all persons");
                var persons = await _personService.GetAllPersonsAsync();
                if (persons == null || !persons.Any())
                {
                    return NotFound(persons);
                }
                return Ok(persons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all persons.");
                return StatusCode(500, "Internal server error");
            }
           
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest("ID must be greater than zero.");
            else
            {
                _logger.LogInformation($"Getting person with ID: {id}");
                var person = await _personService.GetPersonByIdAsync(id);
                if (person == null)
                    return NotFound(person);
                return Ok(person);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            var validator = new PersonValidator();
            var result = validator.Validate(person);
            if (!result.IsValid)
            {
                //return BadRequest(result.Errors.Select(e => new
                //{
                //    Field = e.PropertyName,
                //    Error = e.ErrorMessage
                //}));
                throw new BadRequestException("Validation failed for the person object.", result);

            }
            person.id = await _personService.AddPersonAsync(person);
            return Ok(person);
        }
        [HttpPut]
        public async Task<Person> Put([FromBody] Person person)
        {
            await _personService.UpdatePersonAsync(person);
            return person;

        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _personService.DeletePersonAsync(id);
        }
        


    }
}
