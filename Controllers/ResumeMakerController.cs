using System.Security.Claims;
using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BrainsToDo.Controllers;

[ApiController]
[Route("ResumeMaker")]
[Authorize]

public class ResumeMakerController(ResumeMakerRepository repository, IMapper mapper, DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;
    private int resumeId, userId,  personId; 
    [HttpPost("resume")]
    public async Task<IActionResult> AddResume([FromBody] PostResumeForResumeMaker resumeDTO)
    {
        
         userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
         personId = _context.Person
            .Where(p => p.UserId == userId)
            .Select(p => p.Id)
            .FirstOrDefault();
        
        if (resumeDTO == null)
        {
            return BadRequest("Invalid request");
        }
        
        Resume resume = mapper.Map<Resume>(resumeDTO);
        resume.PersonId = personId;
    
        var createdResume = await repository.AddResume(resume, personId);
        resumeId = createdResume.Id;
        var getResumeDTO = mapper.Map<GetResumeDTO>(createdResume);
        
        return Ok(new Payload<GetResumeDTO>
        {
            Data = getResumeDTO
        });
    }
}

