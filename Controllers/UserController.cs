using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Models;

    [ApiController]
    [Route("user")]
    public class UserController(UserRepository repository, IMapper mapper) : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetAllUsers(IMapper mapper)
        {
            var users = await repository.GetAllEntities();
            if(!users.Any())
            {
                return NotFound("No person found");
            }
            var userDTOs = mapper.Map<IEnumerable<GetUserDTO>>(users);

            var payload = new Payload<IEnumerable<GetUserDTO>>
            {
                Data = userDTOs
            };
            return Ok(payload);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id, IMapper mapper)
        {
            var user = await repository.GetEntityById(id);
            var userDTO = mapper.Map<GetUserDTO>(user);
            
            if(id <= 0)
            {
                return NotFound("Invalid user ID");
            }
            if (user == null)
            {
                return NotFound("User not found");
            }

            var payload = new Payload<GetUserDTO>
            {
                Data = userDTO
            };
            
            return Ok(payload);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser(IMapper mapper, PostUserDTO userDTO) 
        {
            if(userDTO == null)
            {
                return NotFound("Invalid user data");
            };
            
            User user = mapper.Map<User>(userDTO);
            var createdUser = await repository.AddEntity(user);
            
            var getUserDTO = mapper.Map<GetUserDTO>(createdUser);
            var payload = new Payload<GetUserDTO>
            {
                Data = getUserDTO
            };
            
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, payload);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, PostUserDTO userDTO, IMapper mapper)
        {
            User user = mapper.Map<User>(userDTO);
            User updatedUser = await repository.UpdateEntity(id, user);
            
            if(id <= 0)
            {
                return NotFound("Invalid user ID");
            }
            if(user == null)
            {
                return NotFound("Invalid user data");
            }
            if(updatedUser.Equals(user))
            {
                return Ok("No changes detected");
            }
            if(updatedUser == null)
            {
                return NotFound("User not found");
            }
            
            var getUserDTO = mapper.Map<GetUserDTO>(updatedUser);

            var payload = new Payload<GetUserDTO>
            {
                Data = getUserDTO
            };
            
            return Ok(payload);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletedUser(IMapper mapper, int id)
        {
            if(id <= 0)
            {
                return NotFound("Invalid user ID");
            }
            
            var deletedUser = await repository.DeleteEntity(id);
            
            if (deletedUser == null)
            {
                return NotFound("User not found");
            }
            
            var getUserDTO = mapper.Map<GetUserDTO>(deletedUser);

            var payload = new Payload<GetUserDTO>
            {
                Data = getUserDTO
            };
            
            return Ok(payload);
        }
    }