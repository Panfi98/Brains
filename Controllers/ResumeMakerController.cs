using System.Security.Claims;
using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BrainsToDo.Controllers;

[ApiController]
[Route("ResumeMaker")]
[Authorize]

public class ResumeMakerController(ResumeMakerRepository repository, IMapper mapper, DataContext context)
    : ControllerBase
{
    private readonly DataContext _context = context;


    [HttpGet("GetPersonId")]
    public async Task<int> PostResume()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        int personId = _context.Person
            .Where(p => p.UserId == userId)
            .Select(p => p.Id)
            .FirstOrDefault();
        
        return (personId); 
    }

}

