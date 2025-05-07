using System.Security.Claims;
using System.Text;
using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using BrainsToDo.Repositories.LoginLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BrainsToDo.Interfaces;

namespace BrainsToDo.Models;



    [ApiController]
    [Route("user")]
    public class UserController(UserRepository repository, IMapper mapper, LoginRepository loginRepository, IConfiguration configuration, IEmailVerificationService emailVerificationService, DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;
        private readonly IEmailVerificationService _emailVerificationService = emailVerificationService;
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDto)
        {
            try
            {
                var user = await loginRepository.GetUserByUsernameAndPassword(loginDto.Username, loginDto.Password);
            
                if (user == null)
                {
                    return NotFound("Invalid username or password");
                }
                
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("ThisIsYourSecretKeyMakeItAtLeast32CharactersLong");
                
                //Parse stuff from appsettings.json . If not it sets to default stuff
               
                int expirationHours = int.TryParse(configuration["Jwt:ExpireHours"], out var parsed) ? parsed : 3;
                string issuer = configuration["Jwt:Issuer"] ?? "defaultIssuer";
                string audience = configuration["Jwt:Audience"] ?? "defaultAudience";
                
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                    }),
                    Expires = DateTime.UtcNow.AddHours(expirationHours),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
            
                return Ok(new { token = jwtToken });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
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
        [Authorize]
        public async Task<ActionResult> GetUserById(int id)
        {
            var user = await repository.GetEntityById(id);
            var userDTO = mapper.Map<GetUserDTO>(user);

            if (id <= 0)
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
        public async Task<IActionResult> CreateUser(PostUserDTO userDTO)
        {
            if (userDTO == null)
            {
                return NotFound("Invalid user data");
            }

            string userName = userDTO.Name;
            string userEmail = userDTO.Email;
            string password = userDTO.Password;

            if (await repository.UserExists(userName, userEmail))
            {
                return Conflict(new { message = "User with this username or email already exists." });
            }

            // Маппим DTO в сущность User
            User user = mapper.Map<User>(userDTO);

            // Устанавливаем начальные значения для верификации
            user.Confirming = false; // Email не подтвержден
            //user.Code = null;
            //user.ExpirationTime = DateTime.MinValue;
           // user.Attempts = 3;

            // Создаем пользователя в БД
            var createdUser = await repository.AddEntity(user);

            try
            {
                // Генерируем и отправляем код подтверждения
                var code = await _emailVerificationService.GenerateAndSendVerificationCodeAsync(createdUser);

                // Маппим результат в DTO (не включаем конфиденциальные данные)
                var getUserDTO = mapper.Map<GetUserDTO>(createdUser);

                var payload = new Payload<GetUserDTO>
                {
                    Data = getUserDTO,
                    Message = "Код подтверждения отправлен на вашу почту. Пожалуйста, подтвердите email."
                };

                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, payload);
            }
            catch (Exception ex)
            {
                // В случае ошибки отправки кода - удаляем пользователя
                _context.User.Remove(createdUser);
                await _context.SaveChangesAsync();

                return StatusCode(500,
                    new { message = "Ошибка при отправке кода подтверждения. Пожалуйста, попробуйте позже." });
            }
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Code))
            {
                return BadRequest("Необходимо указать email и код подтверждения");
            }

            /*var user = await _context.User.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return NotFound("Пользователь с указанным email не найден");
            }

            var isValid = await _emailVerificationService.VerifyCodeAsync(user, request.Code);
            if (!isValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = user.Attempts > 0
                        ? $"Неверный код подтверждения. Осталось попыток: {user.Attempts}"
                        : "Превышено количество попыток. Запросите новый код.",
                    AttemptsLeft = user.Attempts
                });
            }*/

            // Подтверждаем email
           // user.Confirming = true;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Success = true,
                Message = "Email успешно подтвержден!",
                //UserId = user.Id
            });
        }

        public class ConfirmEmailRequest
        {
            public string Email { get; set; }
            public string Code { get; set; }
        }
        
        [HttpPut("{id}")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> DeletedUser( int id)
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