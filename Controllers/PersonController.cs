using BrainsToDo.Data;
using BrainsToDo.Models;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BrainsToDo.Controllers;

    [ApiController]
    [Route("person")]
    public class PersonController(ICrudRepository<Person> repository) : ControllerBase
    {
        private readonly ICrudRepository<Person> _repository = repository;

        [HttpGet]
        public IActionResult GetAllPersons()
        {
            var persons = _repository.GetAllEntities();
            if(!persons.Any()) return NotFound("No person found");
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult GetPersonById(int id)
        {
            if(id <= 0) return NotFound("Invalid user ID");
            var person = _repository.GetEntityById(id);
            if(person == null) return NotFound("No person found");
            return Ok(person);
        }

        [HttpPost]
        public IActionResult CreatePerson(Person person)
        {
            if(person == null) return NotFound("Invalid request");

            var newPerson = new Person()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber,
                Address = person.Address,
                BirthDate = person.BirthDate,
                PictureURL = person.PictureURL,
                createdAt = person.createdAt,
                updatedAt = person.updatedAt
            };
            
            var createdPerson = _repository.AddEntity(newPerson);
            return CreatedAtAction(nameof(GetPersonById), new { id = newPerson.Id }, newPerson);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, Person person)
        {
            if(id <= 0) return NotFound("Invalid user ID");
            if(person == null) return NotFound("Invalid user data");
            
            var updatedPerson = _repository.GetEntityById(id);
            if(updatedPerson.Equals(person)) return Ok("No changes detected");
            if(updatedPerson == null) return NotFound("No person found");
            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            if(id <= 0) return NotFound("Invalid user ID");
            var deletedPerson = _repository.GetEntityById(id);
            if(deletedPerson == null) return NotFound("No person found");
            return Ok(deletedPerson);
        }
    }