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

            return Created("", new Payload<PostResumeDTO>
                {
                    Data = _mapper.Map<PostResumeDTO>(createdResume),
                    Message = "Resume added successfully"
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

            return Created(" ", new Payload<PostEducationDTO>
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

    [HttpGet("user/{userId}")]
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
    
    [HttpGet("{resumeId}")]
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
    
    [HttpPut("{id}")]
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
    
    [HttpPut("education/{educationId}")]
    public async Task<IActionResult> UpdateEducation( int educationId, [FromBody] PostEducationDTO educationDTO)
    {
        try
        {
            if (educationDTO == null)
            {
                throw new ArgumentNullException(nameof(educationDTO), "Education data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var educationExists = await _context.Education
                .Include(e => e.Resume)
                .AnyAsync(e => e.Id == educationId && e.Resume.UserId == userId);

            if (!educationExists)
            {
                throw new KeyNotFoundException("Resume not found or doesn't belong to current user");
            }

            var education = _mapper.Map<Education>(educationDTO);
            var updatedEducation = await _repository.UpdateEducationByResumeId(educationId, education);
            
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
    
    [HttpPut("certification/{certificationId}")]
    public async Task<IActionResult> UpdateCertification(int certificationId, [FromBody] PostCertificationDTO certificationDTO)
    {
        try
        {
            if (certificationDTO == null)
            {
                throw new ArgumentNullException(nameof(certificationDTO), "Certification data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var certificationExists = await _context.Education
                .Include(e => e.Resume)
                .AnyAsync(e => e.Id == certificationId && e.Resume.UserId == userId);

            if (!certificationExists)
            {
                throw new KeyNotFoundException("Certification not found or doesn't belong to current user");
            }

            var certification = _mapper.Map<Certification>(certificationDTO);
            var updatedCertification = await _repository.UpdateCertificationsByResumeId(certificationId, certification);

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
    
    [HttpPut("project/{projectId}")]
    public async Task<IActionResult> UpdateProject(int projectId, [FromBody] PostProjectDTO projectDTO)
    {
        try
        {
            if (projectDTO == null)
            {
                throw new ArgumentNullException(nameof(projectDTO), "Project data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var projectExists = await _context.Education
                .Include(e => e.Resume)
                .AnyAsync(e => e.Id == projectId && e.Resume.UserId == userId);

            if (!projectExists)
            {
                throw new KeyNotFoundException("Project not found or doesn't belong to current user");
            }

            var project = _mapper.Map<Project>(projectDTO);
            var updatedProject = await _repository.UpdateProjectsByResumeId(projectId, project);

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
    
    [HttpPut("experience/{experienceId}")]
    public async Task<IActionResult> UpdateExperience(int experienceId, [FromBody] PostExperienceDTO experienceDTO)
    {
        try
        {
            if (experienceDTO == null)
            {
                throw new ArgumentNullException(nameof(experienceDTO), "Experience data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var experienceExists = await _context.Education
                .Include(e => e.Resume)
                .AnyAsync(e => e.Id == experienceId && e.Resume.UserId == userId);

            if (!experienceExists)
            {
                throw new KeyNotFoundException("Experience not found or doesn't belong to current user");
            }

            var experience = _mapper.Map<Experience>(experienceDTO);
            var updatedExperience = await _repository.UpdateExperiencesByResumeId(experienceId, experience);

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
    
    [HttpPut("skill/{skillId}")]
    public async Task<IActionResult> UpdateSkill(int skillId, [FromBody] PostInfoSkillDTO skillDTO)
    {
        try
        {
            if (skillDTO == null)
            {
                throw new ArgumentNullException(nameof(skillDTO), "Skill data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var skillExists = await _context.Education
                .Include(e => e.Resume)
                .AnyAsync(e => e.Id == skillId && e.Resume.UserId == userId);

            if (!skillExists)
            {
                throw new KeyNotFoundException("Skill not found or doesn't belong to current user");
            }

            var skill = _mapper.Map<InfoSkill>(skillDTO);
            var updatedSkill = await _repository.UpdateInfoSkillsByResumeId(skillId, skill);

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
    
    [HttpPut("reference/{referenceId}")]
    public async Task<IActionResult> UpdateReference(int referenceId, [FromBody] PostReferenceDTO referenceDTO)
    {
        try
        {
            if (referenceDTO == null)
            {
                throw new ArgumentNullException(nameof(referenceDTO), "Reference data cannot be null");
            }

            var userId = await GetCurrentUserId();
            var referenceExists = await _context.Education
                .Include(e => e.Resume)
                .AnyAsync(e => e.Id == referenceId && e.Resume.UserId == userId);

            if (!referenceExists)
            {
                throw new KeyNotFoundException("Reference not found or doesn't belong to current user");
            }

            var reference = _mapper.Map<Reference>(referenceDTO);
            var updatedReference = await _repository.UpdateReferencesByResumeId(referenceId, reference);

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
    
    [HttpDelete("{id}")]
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

    [HttpDelete("education/{educationId}")]
    public async Task<IActionResult> DeleteEducation(int educationId)
    {
        try
        {
            if (educationId <= 0)
            {
                throw new ArgumentException("Invalid educationId", nameof(educationId));
            }

            var education = await _context.Education.FirstOrDefaultAsync(e => e.Id == educationId);

            if (education == null)
            {
                throw new KeyNotFoundException("Education not found");
            }

            await _repository.DeleteEducation(educationId);

            return NoContent();
        }
        catch(Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpDelete("project/{projectId}")]
    public async Task<IActionResult> DeleteProject(int projectId)
    {
        try
        {
            if (projectId <= 0)
            {
                throw new ArgumentException("Invalid projectId", nameof(projectId));
            }

            var project = await _context.Project.FirstOrDefaultAsync(e => e.Id == projectId);

            if (project == null)
            {
                throw new KeyNotFoundException("Project not found");
            }

            await _repository.DeleteProject(projectId);

            return NoContent();
        }
        catch(Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpDelete("skill/{skillId}")]
    public async Task<IActionResult> DeleteSkill(int skillId)
    {
        try
        {
            if (skillId <= 0)
            {
                throw new ArgumentException("Invalid skillId", nameof(skillId));
            }

            var skill = await _context.Skill.FirstOrDefaultAsync(e => e.Id == skillId);

            if (skill == null)
            {
                throw new KeyNotFoundException("Skill not found");
            }

            await _repository.DeleteSkill(skillId);

            return NoContent();
        }
        catch(Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpDelete("experience/{experienceId}")]
    public async Task<IActionResult> DeleteExperience(int experienceId)
    {
        try
        {
            if (experienceId <= 0)
            {
                throw new ArgumentException("Invalid experienceId", nameof(experienceId));
            }

            var experience = await _context.Experience.FirstOrDefaultAsync(e => e.Id == experienceId);

            if (experience == null)
            {
                throw new KeyNotFoundException("Experience not found");
            }

            await _repository.DeleteExperience(experienceId);

            return NoContent();
        }
        catch(Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpDelete("certification/{certificationId}")]
    public async Task<IActionResult> DeleteCertification(int certificationId)
    {
        try
        {
            if (certificationId <= 0)
            {
                throw new ArgumentException("Invalid certificationId", nameof(certificationId));
            }

            var certification = await _context.Certification.FirstOrDefaultAsync(e => e.Id == certificationId);

            if (certification == null)
            {
                throw new KeyNotFoundException("Certification not found");
            }

            await _repository.DeleteCertification(certificationId);

            return NoContent();
        }
        catch(Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpDelete("reference/{referenceId}")]
    public async Task<IActionResult> DeleteReference(int referenceId)
    {
        try
        {
            if (referenceId <= 0)
            {
                throw new ArgumentException("Invalid referenceId", nameof(referenceId));
            }

            var reference = await _context.Education.FirstOrDefaultAsync(e => e.Id == referenceId);

            if (reference == null)
            {
                throw new KeyNotFoundException("Reference not found");
            }

            await _repository.DeleteReference(referenceId);

            return NoContent();
        }
        catch(Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    private IActionResult HandleException(Exception ex)
    {
        var errorDetails = new
        {
            Message = ex.Message,
            Details = ex.InnerException?.Message,
            ExceptionType = ex.GetType().Name
        };

        return ex switch
        {
            UnauthorizedAccessException => Unauthorized(new
            {
                errorDetails.Message,
                errorDetails.Details,
                StatusCode = StatusCodes.Status401Unauthorized
            }),

            KeyNotFoundException => NotFound(new
            {
                errorDetails.Message,
                errorDetails.Details,
                StatusCode = StatusCodes.Status404NotFound
            }),

            ArgumentException or ArgumentNullException => BadRequest(new
            {
                errorDetails.Message,
                errorDetails.Details,
                StatusCode = StatusCodes.Status400BadRequest
            }),

            DbUpdateException dbEx when dbEx.InnerException?.Message?.Contains("UNIQUE") == true
                => Conflict(new
                {
                    Message = "Duplicate entry",
                    Details = dbEx.InnerException?.Message,
                    StatusCode = StatusCodes.Status409Conflict
                }),

            DbUpdateException => StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Message = "Database error occurred",
                errorDetails.Details,
                StatusCode = StatusCodes.Status500InternalServerError
            }),

            _ => StatusCode(StatusCodes.Status500InternalServerError, new
            {
                errorDetails.Message,
                errorDetails.Details,
                StatusCode = StatusCodes.Status500InternalServerError
            })
        };
    }

}