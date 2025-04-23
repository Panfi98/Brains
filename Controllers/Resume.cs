using System.Security.Claims;
using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Helpers;
using BrainsToDo.Models;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Controllers;

[ApiController]
[Route("ResumeMaker")]
[Authorize]
public class Resume : ControllerBase
{
    private readonly ResumeRepository _repository;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Resume(ResumeRepository repository, DataContext context, IMapper mapper)
    {
        _repository = repository;
        _context = context;
        _mapper = mapper;
    }

    private async Task<int> GetCurrentUserId()
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
        {
            throw new UnauthorizedAccessException("Invalid user ID in token");
        }
        return userId;
    }
    
    [HttpPost("/")]
    public async Task<IActionResult> AddResume([FromBody] PostResumeForResumeMaker resumeDTO)
    {
        try
        {
            if (resumeDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }

            var userId = await GetCurrentUserId();
            var resume = _mapper.Map<Models.Resume>(resumeDTO);
            var createdResume = await _repository.AddResume(resume, userId);

            return Ok(new Payload<PostResumeForResumeMaker>
            {
                Data = _mapper.Map<PostResumeForResumeMaker>(createdResume),
                Message = "Resume created successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("{id} iducation")]
    public async Task<IActionResult> AddEducation(int resumeId, [FromBody] PostEducationForResumeMaker educationDTO)
    {
        try
        {
            if (educationDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }

            
            var education = _mapper.Map<Education>(educationDTO);
            var createdEducation = await _repository.AddEducation(education, personId);

            return Ok(new Payload<PostEducationForResumeMaker>
            {
                Data = _mapper.Map<PostEducationForResumeMaker>(createdEducation),
                Message = "Education added successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("add/certification")]
    public async Task<IActionResult> AddCertification([FromBody] PostCertificationForResumeMaker certificationDTO)
    {
        try
        {
            if (certificationDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }

            var resumeId = await GetCurrentResumeId();
            var certification = _mapper.Map<Certification>(certificationDTO);
            var createdCertification = await _repository.AddCertification(certification, resumeId);

            return Ok(new Payload<PostCertificationForResumeMaker>
            {
                Data = _mapper.Map<PostCertificationForResumeMaker>(createdCertification),
                Message = "Certification added successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("add/experience")]
    public async Task<IActionResult> AddExperience([FromBody] PostExperienceForResumeMaker experienceDTO)
    {
        try
        {
            if (experienceDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }

            var resumeId = await GetCurrentResumeId();
            var experience = _mapper.Map<Experience>(experienceDTO);
            var createdExperience = await _repository.AddExperience(experience, resumeId);

            return Ok(new Payload<PostExperienceForResumeMaker>
            {
                Data = _mapper.Map<PostExperienceForResumeMaker>(createdExperience),
                Message = "Experience added successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("add/project")]
    public async Task<IActionResult> AddProject([FromBody] PostProjectForResumeMaker projectDTO)
    {
        try
        {
            if (projectDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }

            var resumeId = await GetCurrentResumeId();
            var project = _mapper.Map<Project>(projectDTO);
            var createdProject = await _repository.AddProject(project, resumeId);

            return Ok(new Payload<PostProjectForResumeMaker>
            {
                Data = _mapper.Map<PostProjectForResumeMaker>(createdProject),
                Message = "Project added successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("add/skill")]
    public async Task<IActionResult> AddSkill([FromBody] PostSkillForResumeMaker skillDTO)
    {
        try
        {
            if (skillDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }

            var resumeId = await GetCurrentResumeId();
            var personId = await GetCurrentPersonId();

            var educationId = await _context.Education
                .Where(e => e.PersonId == personId)
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            var experienceId = await _context.Experience
                .Where(e => e.ResumeId == resumeId)
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            var projectId = await _context.Project
                .Where(p => p.ResumeId == resumeId)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();

            var skill = _mapper.Map<Skill>(skillDTO);
            var createdSkill = await _repository.AddSkill(skill, resumeId, educationId, experienceId, projectId);

            return Ok(new Payload<PostSkillForResumeMaker>
            {
                Data = _mapper.Map<PostSkillForResumeMaker>(createdSkill),
                Message = "Skill added successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("add/reference")]
    public async Task<IActionResult> AddReference([FromBody] PostReferenceForResumeMaker referenceDTO)
    {
        try
        {
            if (referenceDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }

            var resumeId = await GetCurrentResumeId();
            var reference = _mapper.Map<Reference>(referenceDTO);
            var createdReference = await _repository.AddReference(reference, resumeId);

            return Ok(new Payload<PostReferenceForResumeMaker>
            {
                Data = _mapper.Map<PostReferenceForResumeMaker>(createdReference),
                Message = "Reference added successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    private IActionResult HandleException(Exception ex)
    {
        return ex switch
        {
            UnauthorizedAccessException => Unauthorized(new { ex.Message }),
            KeyNotFoundException => NotFound(new { ex.Message }),
            ArgumentException => BadRequest(new { ex.Message }),
            DbUpdateException => StatusCode(500, new { Message = "Database error occurred" }),
            _ => StatusCode(500, new { Message = "An unexpected error occurred" })
        };
    }
}