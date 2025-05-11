using System.Security.Claims;
using System.Text;
using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BrainsToDo.Interfaces;
using BrainsToDo.Interfaces;

namespace BrainsToDo.Models;

[ApiController]
[Route("User")]
public class UserController : ControllerBase 
{
    private readonly DataContext _context;
   // private readonly IEmailVerificationService _emailVerificationService;
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserController> _logger;
    private readonly ITokenGeneration _tokenGeneration;

    public UserController( UserRepository loginRepository, IMapper mapper, IConfiguration configuration, DataContext context, ILogger<UserController> logger, ITokenGeneration tokenGeneration /* IEmailVerificationService emailVerificationService,*/)
    {
        _context = context;
       // _emailVerificationService = emailVerificationService;
       _userRepository = loginRepository;
        _mapper = mapper;
        _configuration = configuration;
        _logger = logger;
        _tokenGeneration = tokenGeneration;
    }

    [HttpPost]
    [Route("LogIn")]
    public async Task<IActionResult> Login([FromBody] UserLogInDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = await _userRepository.GetUserByUsernameAndPassword(dto.Username, dto.Password);

            if (user == null)
            {
                _logger.LogWarning($"Failed login attempt for username: {dto.Username}");
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var jwtToken = _tokenGeneration.GenerateToken(user);

            _logger.LogInformation($"User {user.Name} logged in successfully");

            return Ok(new
            {
                token = jwtToken,
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login process");
            return StatusCode(500, new { message = "An error occurred while processing your request" });
        }
    }
    
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] UserSignUpDTO signUpDTO)
    {
        try
        {
            var (user, mail, passwordStrength) = await _userRepository.CreateUserWithVerification(
                signUpDTO.Username,
                signUpDTO.Password,
                signUpDTO.Email,
                "NO_VERIFICATION_NEEDED"); 

            _logger.LogInformation($"New user created: {user.Name} with email: {mail.Email}");

            return Ok(new
            {
                Success = true,
                PasswordStrength = passwordStrength.ToString()
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during user creation");
            return StatusCode(500, new { Error = "User creation failed" });
        }
    }
    
}