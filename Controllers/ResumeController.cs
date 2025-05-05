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
        
        var userExists = await _context.User.AnyAsync(u => u.Id == userId);
        if (!userExists)
        {
            throw new KeyNotFoundException("User not found");
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
                throw new ArgumentNullException(nameof(resumeDTO), "Resume data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resume = _mapper.Map<Resume>(resumeDTO);
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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid resume ID", nameof(id));
            }

            if (educationDTO == null)
            {
                throw new ArgumentNullException(nameof(educationDTO), "Education data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == id && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid resume ID", nameof(id));
            }

            if (certificationDTO == null)
            {
                throw new ArgumentNullException(nameof(certificationDTO), "Certification data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == id && r.UserId == userId);
            
            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid resume ID", nameof(id));
            }

            if (experienceDTO == null)
            {
                throw new ArgumentNullException(nameof(experienceDTO), "Experience data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == id && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid resume ID", nameof(id));
            }

            if (projectDTO == null)
            {
                throw new ArgumentNullException(nameof(projectDTO), "Project data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == id && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid resume ID", nameof(id));
            }

            if (skillDTO == null)
            {
                throw new ArgumentNullException(nameof(skillDTO), "Skill data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == id && r.UserId == userId);
            
            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid resume ID", nameof(id));
            }

            if (referenceDTO == null)
            {
                throw new ArgumentNullException(nameof(referenceDTO), "Reference data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == id && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
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
        try
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID", nameof(userId));
            }

            var currentUserId = await GetCurrentUserId();
            
            if (userId != currentUserId)
            {
                throw new UnauthorizedAccessException("You can only view your own resumes");
            }

            var resumes = await _repository.GetAllResumesByUserID(userId);

            if (resumes == null || !resumes.Any())
            {
                throw new KeyNotFoundException("No resumes found for this user");
            }

            var response = new PayloadList<List<GetResumeDTO>>
            {
                Data = _mapper.Map<List<GetResumeDTO>>(resumes),
                Message = "Resumes retrieved successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpGet("resume/{resumeId}")]
    public async Task<IActionResult> GetFullResume(int resumeId)
    {
        try
        {
            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var resume = await _repository.GetFullResume(resumeId);

            if (resume == null)
            {
                throw new KeyNotFoundException("Resume data could not be loaded");
            }

            var response = new PayloadList<GetFullResumesDTO>
            {
                Data = resume,
                Message = "Resume retrieved successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpGet("educations/{resumeId}")]
    public async Task<IActionResult> GetEducationsByResumeId(int resumeId)
    {
        try
        {
            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var educations = await _repository.GetEducationsByResumeId(resumeId);

            if (educations == null || !educations.Any())
            {
                throw new KeyNotFoundException("No educations found for this resume");
            }

            var response = new PayloadList<List<PostEducationDTO>>
            {
                Data = _mapper.Map<List<PostEducationDTO>>(educations),
                Message = "Educations retrieved successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpGet("certifications/{resumeId}")]
    public async Task<IActionResult> GetCertificationsByResumeId(int resumeId)
    {
        try
        {
            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var certifications = await _repository.GetCertificationsByResumeId(resumeId);

            if (certifications == null || !certifications.Any())
            {
                throw new KeyNotFoundException("No certifications found for this resume");
            }

            var response = new PayloadList<List<PostCertificationDTO>>
            {
                Data = _mapper.Map<List<PostCertificationDTO>>(certifications),
                Message = "Certifications retrieved successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpGet("projects/{resumeId}")]
    public async Task<IActionResult> GetProjectsByResumeId(int resumeId)
    {
        try
        {
            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var projects = await _repository.GetProjectsByResumeId(resumeId);

            if (projects == null || !projects.Any())
            {
                throw new KeyNotFoundException("No projects found for this resume");
            }

            var response = new PayloadList<List<PostProjectDTO>>
            {
                Data = _mapper.Map<List<PostProjectDTO>>(projects),
                Message = "Projects retrieved successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpGet("experiences/{resumeId}")]
    public async Task<IActionResult> GetExperiencesByResumeId(int resumeId)
    {
        try
        {
            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var experiences = await _repository.GetExperiencesByResumeId(resumeId);

            if (experiences == null || !experiences.Any())
            {
                throw new KeyNotFoundException("No experiences found for this resume");
            }

            var response = new PayloadList<List<PostExperienceDTO>>
            {
                Data = _mapper.Map<List<PostExperienceDTO>>(experiences),
                Message = "Experiences retrieved successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpGet("skills/{resumeId}")]
    public async Task<IActionResult> GetInfoSkillsByResumeId(int resumeId)
    {
        try
        {
            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var skills = await _repository.GetInfoSkillsByResumeId(resumeId);

            if (skills == null || !skills.Any())
            {
                throw new KeyNotFoundException("No skills found for this resume");
            }

            var response = new PayloadList<List<PostInfoSkillDTO>>
            {
                Data = _mapper.Map<List<PostInfoSkillDTO>>(skills),
                Message = "Skills retrieved successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpGet("references/{resumeId}")]
    public async Task<IActionResult> GetReferencesByResumeId(int resumeId)
    {
        try
        {
            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var references = await _repository.GetReferencesByResumeId(resumeId);

            if (references == null || !references.Any())
            {
                throw new KeyNotFoundException("No references found for this resume");
            }

            var response = new PayloadList<List<PostReferenceDTO>>
            {
                Data = _mapper.Map<List<PostReferenceDTO>>(references),
                Message = "References retrieved successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpPut("resume/{id}")]
    public async Task<IActionResult> UpdateResume(int id, [FromBody] GetResumeDTO resumeDTO)
    {
        try
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid resume ID", nameof(id));
            }

            if (resumeDTO == null)
            {
                throw new ArgumentNullException(nameof(resumeDTO), "Resume data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == id && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var resume = _mapper.Map<Resume>(resumeDTO);
            var updatedResume = await _repository.UpdateResumeById(id, resume);

            if (updatedResume == null)
            {
                throw new Exception("Failed to update resume");
            }

            var response = new Payload<GetResumeDTO>
            {
                Data = _mapper.Map<GetResumeDTO>(updatedResume),
                Message = "Resume updated successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpPut("education/{resumeId}")]
    public async Task<IActionResult> UpdateEducation(int resumeId, [FromBody] PostEducationDTO educationDTO)
    {
        try
        {
            if (educationDTO == null)
            {
                throw new ArgumentNullException(nameof(educationDTO), "Education data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var education = _mapper.Map<Education>(educationDTO);
            var updatedEducation = await _repository.UpdateEducationByResumeId(resumeId, education);

            if (updatedEducation == null)
            {
                throw new Exception("Failed to update education");
            }

            var response = new Payload<PostEducationDTO>
            {
                Data = _mapper.Map<PostEducationDTO>(updatedEducation),
                Message = "Education updated successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpPut("certification/{resumeId}")]
    public async Task<IActionResult> UpdateCertification(int resumeId, [FromBody] PostCertificationDTO certificationDTO)
    {
        try
        {
            if (certificationDTO == null)
            {
                throw new ArgumentNullException(nameof(certificationDTO), "Certification data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var certification = _mapper.Map<Certification>(certificationDTO);
            var updatedCertification = await _repository.UpdateCertificationsByResumeId(resumeId, certification);

            if (updatedCertification == null)
            {
                throw new Exception("Failed to update certification");
            }

            var response = new Payload<PostCertificationDTO>
            {
                Data = _mapper.Map<PostCertificationDTO>(updatedCertification),
                Message = "Certification updated successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpPut("project/{resumeId}")]
    public async Task<IActionResult> UpdateProject(int resumeId, [FromBody] PostProjectDTO projectDTO)
    {
        try
        {
            if (projectDTO == null)
            {
                throw new ArgumentNullException(nameof(projectDTO), "Project data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var project = _mapper.Map<Project>(projectDTO);
            var updatedProject = await _repository.UpdateProjectsByResumeId(resumeId, project);

            if (updatedProject == null)
            {
                throw new Exception("Failed to update project");
            }

            var response = new Payload<PostProjectDTO>
            {
                Data = _mapper.Map<PostProjectDTO>(updatedProject),
                Message = "Project updated successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpPut("experience/{resumeId}")]
    public async Task<IActionResult> UpdateExperience(int resumeId, [FromBody] PostExperienceDTO experienceDTO)
    {
        try
        {
            if (experienceDTO == null)
            {
                throw new ArgumentNullException(nameof(experienceDTO), "Experience data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var experience = _mapper.Map<Experience>(experienceDTO);
            var updatedExperience = await _repository.UpdateExperiencesByResumeId(resumeId, experience);

            if (updatedExperience == null)
            {
                throw new Exception("Failed to update experience");
            }

            var response = new Payload<PostExperienceDTO>
            {
                Data = _mapper.Map<PostExperienceDTO>(updatedExperience),
                Message = "Experience updated successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpPut("skill/{resumeId}")]
    public async Task<IActionResult> UpdateSkill(int resumeId, [FromBody] PostInfoSkillDTO skillDTO)
    {
        try
        {
            if (skillDTO == null)
            {
                throw new ArgumentNullException(nameof(skillDTO), "Skill data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var skill = _mapper.Map<InfoSkill>(skillDTO);
            var updatedSkill = await _repository.UpdateInfoSkillsByResumeId(resumeId, skill);

            if (updatedSkill == null)
            {
                throw new Exception("Failed to update skill");
            }

            var response = new Payload<PostInfoSkillDTO>
            {
                Data = _mapper.Map<PostInfoSkillDTO>(updatedSkill),
                Message = "Skill updated successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpPut("reference/{resumeId}")]
    public async Task<IActionResult> UpdateReference(int resumeId, [FromBody] PostReferenceDTO referenceDTO)
    {
        try
        {
            if (referenceDTO == null)
            {
                throw new ArgumentNullException(nameof(referenceDTO), "Reference data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == resumeId && r.UserId == userId);
            
            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var reference = _mapper.Map<Reference>(referenceDTO);
            var updatedReference = await _repository.UpdateReferencesByResumeId(resumeId, reference);

            if (updatedReference == null)
            {
                throw new Exception("Failed to update reference");
            }

            var response = new Payload<PostReferenceDTO>
            {
                Data = _mapper.Map<PostReferenceDTO>(updatedReference),
                Message = "Reference updated successfully"
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpDelete("resume/{id}")]
    public async Task<IActionResult> DeleteResume(int id)
    {
        try
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid resume ID", nameof(id));
            }

            var userId = await GetCurrentUserId();
            var resumeExists = await _context.Resume.AnyAsync(r => r.Id == id && r.UserId == userId);

            if (!resumeExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            await _repository.DeleteResumeWithRelatedData(id);
            
            return NoContent();
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
            UnauthorizedAccessException => 
                Unauthorized(new { Message = ex.Message }),
                
            KeyNotFoundException => 
                NotFound(new { Message = ex.Message }),
                
            ArgumentException or ArgumentNullException => 
                BadRequest(new { Message = ex.Message }),
                
            DbUpdateException => 
                StatusCode(500, new { Message = "Database error occurred", Details = ex.InnerException?.Message }),
                
            _ => 
                StatusCode(500, new { Message = "An unexpected error occurred", Details = ex.Message })
        };
    }
}