using BrainsToDo.DTOModels;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Mvc;
using BrainsToDo.Helpers;
using AutoMapper;

namespace BrainsToDo.Controllers;

[ApiController]
[Route("SignUp")]
public class UserSignUpController : ControllerBase
{
    private readonly UserSignUpRepository _signUpRepository;
    private readonly ILogger<UserSignUpController> _logger;
    private readonly IMapper _mapper;

    public UserSignUpController( UserSignUpRepository signUpRepository, ILogger<UserSignUpController> logger, IMapper mapper)
    {
        _signUpRepository = signUpRepository;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost("")]
    public async Task<IActionResult> SignUp([FromBody] UserSignUpDTO signUpDTO)
    {
        try
        {
            var (user, mail, passwordStrength) = await _signUpRepository.CreateUserWithVerification(
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