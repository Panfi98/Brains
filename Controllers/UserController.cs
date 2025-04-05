using System.Security.Claims;
using System.Text;
using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using BrainsToDo.Repositories.LoginLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BrainsToDo.Models;

    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController(UserRepository repository, IMapper mapper, LoginRepository loginRepository, IConfiguration configuration) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin login)
        {
            try
            {
                var user = await loginRepository.GetUserByUsernameAndPassword(login.Username, login.Password);
            
                if (user == null)
                {
                    return NotFound("Invalid username or password");
                }
                
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("your_secret_key_here");
                
                //Parse stuff from appsettings.json . If not it sets to default stuff
                int expirationHours = int.TryParse(configuration["Jwt:ExpiryTime"], out var parsed) ? parsed : 3;
                string issuer = configuration["Jwt:Issuer"] ?? "defaultIssuer";
                
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                    }),
                    Expires = DateTime.UtcNow.AddHours(expirationHours),
                    Issuer = "_con",
                    Audience = "yourAudience",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

            
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
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