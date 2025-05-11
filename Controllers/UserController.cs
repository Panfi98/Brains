using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Mvc;
using BrainsToDo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Models;

[ApiController]
[Route("User")]
public class UserController : ControllerBase 
{
    private readonly DataContext _context;
    private readonly IEmailService _emailService;
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserController> _logger;
    private readonly ITokenGeneration _tokenGeneration;

    public UserController( UserRepository loginRepository, IMapper mapper, IConfiguration configuration, DataContext context, ILogger<UserController> logger, ITokenGeneration tokenGeneration, IEmailService emailService)
    {
        _context = context; 
        _emailService = emailService;
       _userRepository = loginRepository;
        _mapper = mapper;
        _configuration = configuration;
        _logger = logger;
        _tokenGeneration = tokenGeneration;
    }

      [HttpPost("LogIn")]
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
            
            var userDto = _mapper.Map<UserLogInDTO>(user);

            return Ok(new
            {
                token = jwtToken,
                user = userDto 
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
                signUpDTO.Email);

            // send the verification code 
            await _emailService.SendVerificationEmailAsync(mail.Email, mail.Code);

            var resultDto = new
            {
                Success = true,
                UserId = user.Id,
                PasswordStrength = passwordStrength.ToString(),
                Message = "A verification code has been sent to your email"
            };

            return Ok(resultDto);
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

    [HttpPost("VerifyEmail/{userId}")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDTO dto)
    {
        try
        {
            var result = await _userRepository.VerifyEmailCodeAsync(dto.UserId, dto.Code);

            if (result.IsSuccess && result.User != null)
            {
                return Ok(_userRepository.CreateSuccessResponse("Email successfully verified!", 
                    new { result.User.Id, result.User.Name }));
            }

            return BadRequest(_userRepository.CreateErrorResponse(result.ErrorMessage ?? "Verification failed"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Email verification error");
            return StatusCode(500, _userRepository.CreateErrorResponse("An error occurred"));
        }
    }
    
    [HttpPost("ResendVerification/{userId}")]
    public async Task<IActionResult> ResendVerificationCode([FromBody] ResendVerificationDTO dto)
    {
        try
        {
            var (user, mail) = await _userRepository.GetUserWithLatestMailAsync(dto.UserId);
        
            if (user == null || mail == null)
            {
                return NotFound(_userRepository.CreateErrorResponse("User or mail record not found"));
            }

            var result = await _userRepository.ResendVerificationCode(dto.UserId, mail.Email);
    
            if (result.success)
            {
                await _emailService.SendVerificationEmailAsync(mail.Email, result.message);
                return Ok(_userRepository.CreateSuccessResponse("New verification code sent"));
            }

            return BadRequest(_userRepository.CreateErrorResponse(result.message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error resending verification code");
            return StatusCode(500, _userRepository.CreateErrorResponse("Error resending code"));
        }
    }
}
