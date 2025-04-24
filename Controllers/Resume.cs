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
[Route("resume")]
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
    
    [HttpPost("")]
    public async Task<IActionResult> AddResume([FromBody] PostResumeDTO resumeDTO)
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

            return Ok(new Payload<PostResumeDTO>
            {
                Data = _mapper.Map<PostResumeDTO>(createdResume),
                Message = "Resume created successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    
    [HttpPost("{id}/education")]
    public async Task<IActionResult> AddEducation(int id, [FromBody] PostEducationDTO educationDTO)
    {
        try
        {
            if (educationDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }

            
            var education = _mapper.Map<Education>(educationDTO);
            var createdEducation = await _repository.AddEducation(education, id);

            return Ok(new Payload<PostEducationDTO>
            {
                Data = _mapper.Map<PostEducationDTO>(createdEducation),
                Message = "Education added successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("{id}/certification")]
    public async Task<IActionResult> AddCertification(int id, [FromBody] PostCertificationDTO certificationDTO)
    {
        try
        {
            if (certificationDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }
            
            var certification = _mapper.Map<Certification>(certificationDTO);
            var createdCertification = await _repository.AddCertification(certification, id);

            return Ok(new Payload<PostCertificationDTO>
            {
                Data = _mapper.Map<PostCertificationDTO>(createdCertification),
                Message = "Certification added successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("{id}/experience")]
    public async Task<IActionResult> AddExperience(int id, [FromBody] PostExperienceDTO experienceDTO)
    {
        try
        {
            if (experienceDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }
            
            var experience = _mapper.Map<Experience>(experienceDTO);
            var createdExperience = await _repository.AddExperience(experience, id);

            return Ok(new Payload<PostExperienceDTO>
            {
                Data = _mapper.Map<PostExperienceDTO>(createdExperience),
                Message = "Experience added successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("{id}/project")]
    public async Task<IActionResult> AddProject(int id, [FromBody] PostProjectDTO projectDTO)
    {
        try
        {
            if (projectDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }
            
            var project = _mapper.Map<Project>(projectDTO);
            var createdProject = await _repository.AddProject(project, id);

            return Ok(new Payload<PostProjectDTO>
            {
                Data = _mapper.Map<PostProjectDTO>(createdProject),
                Message = "Project added successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("{id}/skill")]
    public async Task<IActionResult> AddSkill(int id, [FromBody] PostSkillDTO skillDTO)
    {
        try
        {
            if (skillDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }
            
            var educationId = await _context.Education
                .Where(e => e.ResumeId == id)
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            var experienceId = await _context.Experience
                .Where(e => e.ResumeId == id)
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            var projectId = await _context.Project
                .Where(p => p.ResumeId == id)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();

            var skill = _mapper.Map<Skill>(skillDTO);
            var createdSkill = await _repository.AddSkill(skill, id, educationId, experienceId, projectId);

            return Ok(new Payload<PostSkillDTO>
            {
                Data = _mapper.Map<PostSkillDTO>(createdSkill),
                Message = "Skill added successfully"
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("{id}/reference")]
    public async Task<IActionResult> AddReference(int id, [FromBody] PostReferenceDTO referenceDTO)
    {
        try
        {
            if (referenceDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }
            
            var reference = _mapper.Map<Reference>(referenceDTO);
            var createdReference = await _repository.AddReference(reference, id);

            return Ok(new Payload<PostReferenceDTO>
            {
                Data = _mapper.Map<PostReferenceDTO>(createdReference),
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