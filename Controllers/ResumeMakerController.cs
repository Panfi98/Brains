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
        return _context.Resume
            .Where(r => r.PersonId == GetCurrentPersonId())
            .Select(r => r.Id)
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
        var createdResume = await repository.AddResume(resume, GetCurrentPersonId());
        var responseDto = mapper.Map<PostResumeForResumeMaker>(createdResume);

        return Ok(new Payload<PostResumeForResumeMaker>
        {
            Data = responseDto
        });
    }
    
    [HttpPost("education")]
    public async Task<IActionResult> AddEducation([FromBody] PostEducationForResumeMaker educationDTO)
    {
        if (educationDTO == null)
        {
            return BadRequest("Invalid request");
        }
        
        Education education = mapper.Map<Education>(educationDTO);
        var createdEducation = await repository.AddEducation(education, GetCurrentPersonId());
        var responseDto = mapper.Map<PostEducationForResumeMaker>(createdEducation);

        return Ok(new Payload<PostEducationForResumeMaker>
        {
            Data = responseDto
        });
    }
    
    [HttpPost("certification")]
    public async Task<IActionResult> AddCertigication([FromBody] PostCertificationForResumeMaker certificationDTO)
    {
        if (certificationDTO == null)
        {
            return BadRequest("Invalid request");
        }
        
        Certification certification = mapper.Map<Certification>(certificationDTO);
        var createdCertification = await repository.AddCertification(certification, GetCurrentResumeId());
        var responseDto = mapper.Map<PostCertificationForResumeMaker>(createdCertification);

        return Ok(new Payload<PostCertificationForResumeMaker>
        {
            Data = responseDto
        });
    }
    
    [HttpPost("experience")]
    public async Task<IActionResult> AddExperience([FromBody] PostExperienceForResumeMaker experienceDTO)
    {
        if (experienceDTO == null)
        {
            return BadRequest("Invalid request");
        }
        
        Experience experience = mapper.Map<Experience>(experienceDTO);
        var createdExperience = await repository.AddExperience(experience, GetCurrentResumeId());
        var responseDto = mapper.Map<PostExperienceForResumeMaker>(createdExperience);

        return Ok(new Payload<PostExperienceForResumeMaker>
        {
            Data = responseDto
        });
    }
    
    [HttpPost("project")]
    public async Task<IActionResult> AddProject([FromBody] PostProjectForResumeMaker projectDTO)
    {
        if (projectDTO == null)
        {
            return BadRequest("Invalid request");
        }
        
        Project project = mapper.Map<Project>(projectDTO);
        var createdProject = await repository.AddProject(project, GetCurrentResumeId());
        var responseDto = mapper.Map<PostProjectForResumeMaker>(createdProject);

        return Ok(new Payload<PostProjectForResumeMaker>
        {
            Data = responseDto
        });
    }
    
    [HttpPost("skill")]
    public async Task<IActionResult> AddSkill([FromBody] PostSkillForResumeMaker skillDTO)
    {
        int educationId = _context.Education
            .Where(e => e.PersonId == GetCurrentPersonId())
            .Select(e => e.Id)
            .FirstOrDefault();
        
        int experienceId = _context.Experience
            .Where(e => e.ResumeId == GetCurrentResumeId())
            .Select(e => e.Id)
            .FirstOrDefault();
        
        int projectId = _context.Project
            .Where(p => p.ResumeId == GetCurrentResumeId())
            .Select(p => p.Id)
            .FirstOrDefault();
        
        if (skillDTO == null)
        {
            return BadRequest("Invalid request");
        }
        
        Skill skill = mapper.Map<Skill>(skillDTO);
        var createdSkill = await repository.AddSkill(skill, GetCurrentResumeId(),  educationId, experienceId, projectId);
        var responseDto = mapper.Map<PostSkillForResumeMaker>(createdSkill);

        return Ok(new Payload<PostSkillForResumeMaker>
        {
            Data = responseDto
        });
    }
    
    [HttpPost("reference")]
    public async Task<IActionResult> AddReference([FromBody] PostReferenceForResumeMaker referencetDTO)
    {
        if (referencetDTO == null)
        {
            return BadRequest("Invalid request");
        }
        
        Reference reference = mapper.Map<Reference>(referencetDTO);
        var createdReference = await repository.AddReference(reference, GetCurrentResumeId());
        var responseDto = mapper.Map<PostReferenceForResumeMaker>(createdReference);

        return Ok(new Payload<PostReferenceForResumeMaker>
        {
            Data = responseDto
        });
    }
}

