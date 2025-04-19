using System.Security.Claims;
using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BrainsToDo.Controllers;

[ApiController]
[Route("ResumeMaker")]
[Authorize]

public class ResumeMakerController(ResumeMakerRepository repository, IMapper mapper, DataContext context)
    : ControllerBase
{
    private readonly DataContext _context = context;
    private int resumeId, userId;
    
    private int GetCurrentPersonId()
    {
        userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return _context.Person
            .Where(p => p.UserId == userId)
            .Select(p => p.Id)
            .FirstOrDefault();
    }
    
    private int GetCurrentResumeId()
    {
        
        return _context.Person
            .Where(p => p.UserId == userId)
            .Select(p => p.Id)
            .FirstOrDefault();
    }

    [HttpPost("resume")]
    public async Task<IActionResult> AddResume([FromBody] PostResumeForResumeMaker resumeDTO)
    {
        
        if (resumeDTO == null)
        {
            return BadRequest("Invalid request");
        }

        Resume resume = mapper.Map<Resume>(resumeDTO);
        resume.PersonId = GetCurrentPersonId();

        var createdResume = await repository.AddResume(resume);
        resumeId = createdResume.Id;
        var getResumeDTO = mapper.Map<GetResumeDTOForResumeMaker>(createdResume);

        return Ok(new Payload<GetResumeDTOForResumeMaker>
        {
            Data = getResumeDTO
        });
    }
    [HttpPost("education")]
    public async Task<IActionResult> AddEducation([FromBody] PostEducationForResumeMaker educationDTO)
    {
        if (educationDTO == null) return BadRequest("Invalid request");

        
        Education education = mapper.Map<Education>(educationDTO);
        education.PersonId = GetCurrentPersonId();
        var createdEducation = await repository.AddEducation(education);
        var responseDto = mapper.Map<GetEducationDTOForResumeMaker>(createdEducation);

        return Ok(new Payload<GetEducationDTOForResumeMaker>
        {
            Data = responseDto
        });
    }
}

