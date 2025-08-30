using Base.Application.Logging;
using Base.Application.ServiceContracts;
using Base.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<Person>> Get()
        {
            _logger.LogInformation("Getting all persons");
            return await _personService.GetAllPersonsAsync();
        }
        [HttpPost]
        public async Task<Person> Post([FromBody] Person person)
        {
            person.id= await _personService.AddPersonAsync(person);
            return person;
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
        [HttpGet("{id}")]
        public async Task<Person> Get(int id)
        {
            return await _personService.GetPersonByIdAsync(id);
        }


    }
}
