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
    private int IdForPerson = 0;

    public int FindOutIdPerson ()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        IdForPerson = _context.Person
            .Where(p => p.UserId == userId)
            .Select(p => p.Id)
            .FirstOrDefault();
        
        return (IdForPerson); 
    }

    
    
    [HttpPost("add-resume/{personId}")]
    public async Task<IActionResult> AddResume([FromBody] ResumeMakerDTO dto, int personId)
    {
        personId = IdForPerson;
        var result = await repository.AddResume(dto, personId);
        return Ok(result);
    }

    [HttpPost("add-template")]
    public async Task<IActionResult> AddTemplate([FromBody] ResumeMakerDTO dto)
    {
        var result = await repository.AddResumeTemplate(dto);
        return Ok(result);
    }

    [HttpPost("add-education/{personId}")]
    public async Task<IActionResult> AddEducation([FromBody] ResumeMakerDTO dto, int personId)
    {
        var result = await repository.AddEducationList(dto, personId);
        return Ok(result);
    }

    [HttpPost("add-certifications")]
    public async Task<IActionResult> AddCertifications([FromBody] ResumeMakerDTO dto)
    {
        var result = await repository.AddCertifications(dto);
        return Ok(result);
    }

    [HttpPost("add-experience")]
    public async Task<IActionResult> AddExperience([FromBody] ResumeMakerDTO dto)
    {
        var result = await repository.AddExperienceList(dto);
        return Ok(result);
    }

    [HttpPost("add-projects")]
    public async Task<IActionResult> AddProjects([FromBody] ResumeMakerDTO dto)
    {
        var result = await repository.AddProjects(dto);
        return Ok(result);
    }

    [HttpPost("add-skills")]
    public async Task<IActionResult> AddSkills([FromBody] ResumeMakerDTO dto)
    {
        var result = await repository.AddSkills(dto);
        return Ok(result);
    }

    [HttpPost("add-references")]
    public async Task<IActionResult> AddReferences([FromBody] ResumeMakerDTO dto)
    {
        var result = await repository.AddReferences(dto);
        return Ok(result);
    }
    
}

