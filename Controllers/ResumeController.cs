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
public class ResumeController : ControllerBase
{
    private readonly ResumeRepository _repository;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ResumeController(ResumeRepository repository, DataContext context, IMapper mapper)
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

            var response = new Payload<PostResumeDTO>
            {
                Data = _mapper.Map<PostResumeDTO>(createdResume),
                Message = "Resume created successfully"
            };

            return Created("", response);
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

            return Created("", new Payload<PostEducationDTO>
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

            return Created("", new Payload<PostCertificationDTO>
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

            return Created("", new Payload<PostExperienceDTO>
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

            return Created("", new Payload<PostProjectDTO>
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
    public async Task<IActionResult> AddSkill(int id, [FromBody] PostInfoSkillDTO skillDTO)
    {
        try
        {
            if (skillDTO == null)
            {
                return BadRequest(new { Message = "Request body cannot be empty" });
            }
            

            var skill = _mapper.Map<InfoSkill>(skillDTO);
            var createdSkill = await _repository.AddSkill(skill, id);

            return Created("", new Payload<PostInfoSkillDTO>
            {
                Data = _mapper.Map<PostInfoSkillDTO>(createdSkill),
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

            return Created("", new Payload<PostReferenceDTO>
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

    [HttpGet("resumes/{userId}")]
    public async Task<IActionResult> GetAllResumes(int userId)
    {
        
        var user = await _context.User.FindAsync(userId);
       
        if (user == null)
        {
            return NotFound("User not found");
        }
        
        var resumes = await _repository.GetAllResumesByUserID(userId);

        if (resumes == null)
        {
            return NotFound("Resumes not found.");
        }

        return Ok(_mapper.Map<List<GetResumeDTO>>(resumes));
    }
    
    [HttpGet("resume/{resumeId}")]
    public async Task<IActionResult> GetFullResume(int resumeId)
    {
        var resume = await _repository.GetFullResume(resumeId);

        if (resume == null)
        {
            return NotFound("Resume not found.");
        }

        return Ok(resume);
    }
    
    [HttpGet ("/educations/{resumeId}")]
    public async Task<ActionResult<PostEducationDTO>> GetEducationsByResumeIdController(int resumeId)
    {
        var educations = await _repository.GetEducationsByResumeId(resumeId);
        return Ok(_mapper.Map<List<PostEducationDTO>>(educations));
    }
    
    [HttpGet ("/certifications/{resumeId}")]
    public async Task<ActionResult<PostCertificationDTO>> GetCertificationsByResumeIdController(int resumeId)
    {
        var certification = await _repository.GetCertificationsByResumeId(resumeId);
        return Ok(_mapper.Map<List<PostCertificationDTO>>(certification));
    }
    
    [HttpGet ("/projects/{resumeId}")]
    public async Task<ActionResult<PostProjectDTO>> GetProjectsByResumeIdController(int resumeId)
    {
        var project = await _repository.GetProjectsByResumeId(resumeId);
        return Ok(_mapper.Map<List<PostProjectDTO>>(project));
    }
    
    [HttpGet ("/experiences/{resumeId}")]
    public async Task<ActionResult<PostExperienceDTO>> GetExperiencesByResumeIdController(int resumeId)
    {
        var experience = await _repository.GetExperiencesByResumeId(resumeId);
        return Ok(_mapper.Map<List<PostExperienceDTO>>(experience));
    }
    
    [HttpGet ("/skills/{resumeId}")]
    public async Task<ActionResult<PostInfoSkillDTO>> GetInfoSkillsByResumeIdController(int resumeId)
    {
        var skill = await _repository.GetInfoSkillsByResumeId(resumeId);
        return Ok(_mapper.Map<List<PostInfoSkillDTO>>(skill));
    }
    
    [HttpGet ("/references/{resumeId}")]
    public async Task<ActionResult<PostReferenceDTO>> GetReferencesByResumeIdController(int resumeId)
    {
        var reference = await _repository.GetReferencesByResumeId(resumeId);
        return Ok(_mapper.Map<List<PostReferenceDTO>>(reference));
    }
    
    [HttpDelete("resume/{id}")]
    public async Task<IActionResult> DeleteResume(int id)
    {
        try
        {
            await _repository.DeleteResumeWithRelatedData(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);;
        }
    }
    
    private  IActionResult HandleException(Exception ex)
    {
        return ex switch
        {
            UnauthorizedAccessException => 
                Unauthorized(new { Message = ex.Message }),
                
            KeyNotFoundException => 
                NotFound(new { Message = ex.Message }),
                
            ArgumentException or ArgumentNullException => 
                BadRequest(new { Message = ex.Message }),
                
            DbUpdateException => 
                StatusCode(500, new { Message = "Database error occurred" }),
                
             _=> 
                StatusCode(500, new { Message = "An unexpected error occurred" })
        };
    }
}
