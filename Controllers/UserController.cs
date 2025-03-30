using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Models;

    [ApiController]
    [Route("user")]
    public class UserController(ICrudRepository<User> repository) : ControllerBase
    {
        private readonly ICrudRepository<User> _repository = repository;

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _repository.GetAllEntities();
            if(!users.Any()) return NotFound("No users found");
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            if(id <= 0) return NotFound("Invalid user ID");
            var user = _repository.GetEntityById(id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }
        [HttpPost]
        public IActionResult CreateUser(User user) 
        {
            if(user == null) return NotFound("Invalid user data");

            var newUser = new User()
            {
                Name = user.Name,
                Password = user.Password,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow,
            };
            
            var createdUser = _repository.AddEntity(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if(id <= 0) return NotFound("Invalid user ID");
            if(user == null) return NotFound("Invalid user data");
            
            var updatedUser = _repository.UpdateEntity(id, user);
            if(updatedUser.Equals(user)) return Ok("No changes detected");
            if(updatedUser == null) return NotFound("User not found");
            return Ok(updatedUser);
        }

        [HttpDelete]
        public IActionResult DeletedUser(int id)
        {
            if(id <= 0) return NotFound("Invalid user ID");
            var deletedUser = _repository.DeleteEntity(id);
            if (deletedUser == null) return NotFound("User not found");
            return Ok(deletedUser);
        }

}