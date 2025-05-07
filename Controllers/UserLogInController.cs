using System.Security.Claims;
using System.Text;
using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BrainsToDo.Interfaces;


namespace BrainsToDo.Models;

[ApiController]
[Route("LogIn")]
public class UserLogInController : ControllerBase 
{
    private readonly DataContext _context;
   // private readonly IEmailVerificationService _emailVerificationService;
    private readonly UserLogInRepository _loginRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserLogInController> _logger;

    public UserLogInController(
        UserLogInRepository loginRepository, 
        IMapper mapper, 
        IConfiguration configuration, 
       // IEmailVerificationService emailVerificationService, 
        DataContext context,
        ILogger<UserLogInController> logger)
    {
        _context = context;
       // _emailVerificationService = emailVerificationService;
        _loginRepository = loginRepository;
        _mapper = mapper;
        _configuration = configuration;
        _logger = logger;
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = await _loginRepository.GetUserByUsernameAndPassword(loginDto.Username, loginDto.Password);
        
            if (user == null)
            {
                _logger.LogWarning($"Failed login attempt for username: {loginDto.Username}");
                return Unauthorized(new { message = "Invalid username or password" });
            }
            
            /*if (!user.IsEmailVerified)
            {
                return Unauthorized(new { message = "Please verify your email first" });
            }*/

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured"));
            
            int expirationHours = int.TryParse(_configuration["Jwt:ExpireHours"], out var parsed) ? parsed : 3;
            string issuer = _configuration["Jwt:Issuer"] ?? "defaultIssuer";
            string audience = _configuration["Jwt:Audience"] ?? "defaultAudience";
            
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
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            
            _logger.LogInformation($"User {user.Name} logged in successfully");
            
            return Ok(new { 
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
}