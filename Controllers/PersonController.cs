using AutoMapper;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainsToDo.Controllers;

    [ApiController]
    [Route("person")]
    [Authorize]
    public class PersonController(PersonRepository repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPersons(IMapper mapper)
        {
            var persons = await repository.GetAllEntities();
            
            if (!persons.Any())
            {
                return NotFound("No person found");
            }

            var personDTOs = mapper.Map<IEnumerable<GetPersonDTO>>(persons);

            var payload = new Payload<IEnumerable<GetPersonDTO>>()
            {
                Data = personDTOs
            };
            
            return Ok(payload);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPersonById(int id, IMapper mapper)
        {
            var person = await repository.GetEntityById(id);
            var personDTO = mapper.Map<GetPersonDTO>(person);
            
            if(id <= 0)
            {
                return NotFound("Invalid user ID");
            }
            if(person == null)
            {
                return NotFound("No person found");
            }

            var payload = new Payload<GetPersonDTO>()
            {
                Data = personDTO
            };
            
            return Ok(payload);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePerson(PostPersonDTO personDTO, IMapper mapper)
        {
            if(personDTO == null)
            {
                return NotFound("Invalid request");
            }
            
            Person person = mapper.Map<Person>(personDTO);
            person.UserId = personDTO.UserId;
            
            var createdPerson = await repository.AddEntity(person);
            
            var getPersonDTO = mapper.Map<GetPersonDTO>(createdPerson);

            var payload = new Payload<GetPersonDTO>()
            {
                Data = getPersonDTO
            };
            
            return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.Id }, payload);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePerson(int id, PostPersonDTO personDTO, IMapper mapper)
        {
            Person person = mapper.Map<Person>(personDTO);
            Person updatedPerson = await repository.UpdateEntity(id, person);
            
            if(id <= 0)
            {
                return NotFound("Invalid user ID");
            }
            if(person == null)
            {
                return NotFound("Invalid user data");
            }
            if(updatedPerson.Equals(person))
            {
                return Ok("No changes detected");
            }
            if(updatedPerson == null)
            {
                return NotFound("No person found");
            }
            
            var getPersonDTO = mapper.Map<GetPersonDTO>(updatedPerson);

            var payload = new Payload<GetPersonDTO>()
            {
                Data = getPersonDTO
            };
            
            return Ok(payload);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id, IMapper mapper)
        {
            if(id <= 0)
            {
                return NotFound("Invalid user ID");
            }
            
            var deletedPerson = await repository.DeleteEntity(id);
            
            if(deletedPerson == null)
            {
                return NotFound("No person found");
            }
            
            var getPersonDTO = mapper.Map<GetPersonDTO>(deletedPerson);

            var payload = new Payload<GetPersonDTO>()
            {
                Data = getPersonDTO
            };
            
            return Ok(payload);
        }
    }