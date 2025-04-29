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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
    public async Task<IActionResult> GetAllResumes(int userId)
    {
        
        var user = await _context.User.FindAsync(userId);
       
        if (user == null)
        {
            return NotFound("User not found");
        }
        
        var ckeckResumes = await _repository.GetAllResumesByUserID(userId);

        if (ckeckResumes == null)
        {
            return NotFound("Resumes not found.");
        }
        
        var resumes = _mapper.Map<List<GetResumeDTO>>(ckeckResumes);
        var payload = new PayloadList<List<GetResumeDTO>>
        {
            Data = resumes
        };
        
        return Ok(payload);
    }
    
    [HttpGet("resume/{resumeId}")]
    [Authorize]
    public async Task<IActionResult> GetFullResume(int resumeId)
    {
        var resume = await _repository.GetFullResume(resumeId);

        if (resume == null)
        {
            return NotFound("Resume not found.");
        }

        var payload = new PayloadList<GetFullResumesDTO>
        {
            Data = resume
        };
        return Ok(payload);
    }
    
    [HttpGet ("/educations/{resumeId}")]
    [Authorize]
    public async Task<ActionResult<PostEducationDTO>> GetEducationsByResumeIdController(int resumeId)
    {
        var CheckedeEducations = await _repository.GetEducationsByResumeId(resumeId);
        var educations= _mapper.Map<List<PostEducationDTO>>(CheckedeEducations);

        var payload = new PayloadList<List<PostEducationDTO>>
        {
            Data = educations
        };
        
        return Ok(payload);
    }
    
    [HttpGet ("/certifications/{resumeId}")]
    public async Task<ActionResult<PostCertificationDTO>> GetCertificationsByResumeIdController(int resumeId)
    {
        var CheckedCertification = await _repository.GetCertificationsByResumeId(resumeId);
        var certifictions = _mapper.Map<List<PostCertificationDTO>>(CheckedCertification);

        var payload = new PayloadList<List<PostCertificationDTO>>
        {
            Data = certifictions
        };
        
        return Ok(payload);

    }
    
    [HttpGet ("/projects/{resumeId}")]
    [Authorize]
    public async Task<ActionResult<PostProjectDTO>> GetProjectsByResumeIdController(int resumeId)
    {
        var CheckedProjects = await _repository.GetProjectsByResumeId(resumeId);
        var projects = _mapper.Map<List<PostProjectDTO>>(CheckedProjects);

        var payload = new PayloadList<List<PostProjectDTO>>
        {
            Data = projects
        };
         
         return Ok(payload);
    }
    
    [HttpGet ("/experiences/{resumeId}")]
    public async Task<ActionResult<PostExperienceDTO>> GetExperiencesByResumeIdController(int resumeId)
    {
        var CheckedExperiences = await _repository.GetExperiencesByResumeId(resumeId);
        var experiences = _mapper.Map<List<PostExperienceDTO>>(CheckedExperiences);
        
        var payload = new PayloadList<List<PostExperienceDTO>>
         {
             Data = experiences
         };
         
         return Ok(payload);
    }
    
    [HttpGet ("/skills/{resumeId}")]
    [Authorize]
    public async Task<ActionResult<PostInfoSkillDTO>> GetInfoSkillsByResumeIdController(int resumeId)
    {
        var CheckedSkills = await _repository.GetInfoSkillsByResumeId(resumeId);
        var skills =_mapper.Map<List<PostInfoSkillDTO>>(CheckedSkills);
        
        var payload = new PayloadList<List<PostInfoSkillDTO>>
        {
            Data = skills
        };
         
        return Ok(payload);
    }
    
    [HttpGet ("/references/{resumeId}")]
    public async Task<ActionResult<PostReferenceDTO>> GetReferencesByResumeIdController(int resumeId)
    {
        var CheckedReferences = await _repository.GetReferencesByResumeId(resumeId);
        var references = _mapper.Map<List<PostReferenceDTO>>(CheckedReferences);
        
        var payload = new PayloadList<List<PostReferenceDTO>>
        {
            Data = references
        };
         
        return Ok(payload);
    }
    
    [HttpPut("resume/{Id}")]
    [Authorize]
    public async Task<IActionResult> UpdateResumeByResumeIdController(int Id, GetResumeDTO  resumeDTO, IMapper mapper)
    {
        Resume resume = mapper.Map<Resume>(resumeDTO);
        Resume updatedResume = await _repository.UpdateResumeById(Id, resume);
            
        if(Id <= 0)
        {
            return NotFound("Invalid user ID");
        }
        if(resume == null)
        {
            return NotFound("Invalid data");
        }
        if(updatedResume.Equals(resume))
        {
            return Ok("No changes detected");
        }
        if(updatedResume == null)
        {
            return NotFound("Resume not found");
        }
            
        var getResumeDTO = mapper.Map<GetResumeDTO>(updatedResume);

        var payload = new Payload<GetResumeDTO>
        {
            Message = "Resume updated successfully.",
            Data = getResumeDTO
        };
            
        return Ok(payload);
    }
    
    [HttpPut("education/{resumeId}")]
    [Authorize]
    public async Task<IActionResult> UpdateEducationByResumeIdController(int resumeId, PostEducationDTO  educationDTO, IMapper mapper)
    {
        Education education = mapper.Map<Education>(educationDTO);
        Education updatedEducation = await _repository.UpdateEducationByResumeId(resumeId, education);
            
        if(resumeId <= 0)
        {
            return NotFound("Invalid user ID");
        }
        if(education == null)
        {
            return NotFound("Invalid data");
        }
        if(updatedEducation.Equals(education))
        {
            return Ok("No changes detected");
        }
        if(updatedEducation == null)
        {
            return NotFound("Education not found");
        }
            
        var getEducationDTO = mapper.Map<PostEducationDTO>(updatedEducation);

        var payload = new Payload<PostEducationDTO>
        {
            Message = "Education updated successfully.",
            Data = getEducationDTO
        };
            
        return Ok(payload);
    }
    
    [HttpPut("certification/{resumeId}")]
    [Authorize]
    public async Task<IActionResult> UpdateCertificationByResumeIdController(int resumeId, PostCertificationDTO  educationDTO, IMapper mapper)
    {
        Certification  certification = mapper.Map<Certification>(educationDTO);
        Certification updatedCertification = await _repository.UpdateCertificationsByResumeId(resumeId, certification);
            
        if(resumeId <= 0)
        {
            return NotFound("Invalid ID");
        }
        if(certification == null)
        {
            return NotFound("Invalid data");
        }
        if(updatedCertification.Equals(certification))
        {
            return Ok("No changes detected");
        }
        if(updatedCertification == null)
        {
            return NotFound("Certification not found");
        }
            
        var getCertificationDTO = mapper.Map<PostCertificationDTO>(updatedCertification);

        var payload = new Payload<PostCertificationDTO>
        {
            Message = "Certification updated successfully.",
            Data = getCertificationDTO
        };
            
        return Ok(payload);
    }
    
    [HttpPut("project/{resumeId}")]
    [Authorize]
    public async Task<IActionResult> UpdateProjectByResumeIdController(int resumeId, PostProjectDTO  projectDTO, IMapper mapper)
    {
        Project  project = mapper.Map<Project>(projectDTO);
        Project updatedProject = await _repository.UpdateProjectsByResumeId(resumeId, project);
            
        if(resumeId <= 0)
        {
            return NotFound("Invalid ID");
        }
        if(project == null)
        {
            return NotFound("Invalid data");
        }
        if(updatedProject.Equals(project))
        {
            return Ok("No changes detected");
        }
        if(updatedProject == null)
        {
            return NotFound("Project not found");
        }
            
        var getProjectDTO = mapper.Map<PostProjectDTO>(updatedProject);

        var payload = new Payload<PostProjectDTO>
        {
            Message = "Project updated successfully.",
            Data = getProjectDTO
        };
            
        return Ok(payload);
    }
    
    [HttpPut("experience/{resumeId}")]
    [Authorize]
    public async Task<IActionResult> UpdateExperienceByResumeIdController(int resumeId, PostExperienceDTO  experienceDTO, IMapper mapper)
    {
        Experience  experience = mapper.Map<Experience>(experienceDTO);
        Experience updatedExperience = await _repository.UpdateExperiencesByResumeId(resumeId, experience);
            
        if(resumeId <= 0)
        {
            return NotFound("Invalid ID");
        }
        if(experience == null)
        {
            return NotFound("Invalid data");
        }
        if(updatedExperience.Equals(experience))
        {
            return Ok("No changes detected");
        }
        if(updatedExperience == null)
        {
            return NotFound("Experience not found");
        }
            
        var getExperienceDTO = mapper.Map<PostExperienceDTO>(updatedExperience);

        var payload = new Payload<PostExperienceDTO>
        {
            Message = "Experience updated successfully.",
            Data = getExperienceDTO
        };
            
        return Ok(payload);
    }
    
    [HttpPut("skill/{resumeId}")]
    [Authorize]
    public async Task<IActionResult> UpdateInfoSkillByResumeIdController(int resumeId, PostInfoSkillDTO  skillDTO, IMapper mapper)
    {
        InfoSkill  skill = mapper.Map<InfoSkill>(skillDTO);
        InfoSkill updatedSkill = await _repository.UpdateInfoSkillsByResumeId(resumeId, skill);
            
        if(resumeId <= 0)
        {
            return NotFound("Invalid ID");
        }
        if(skill == null)
        {
            return NotFound("Invalid data");
        }
        if(updatedSkill.Equals(skill))
        {
            return Ok("No changes detected");
        }
        if(updatedSkill == null)
        {
            return NotFound("Skill not found");
        }
            
        var getSkillDTO = mapper.Map<PostInfoSkillDTO>(updatedSkill);

        var payload = new Payload<PostInfoSkillDTO>
        {
            Message = "Skill updated successfully.",
            Data = getSkillDTO
        };
            
        return Ok(payload);
    }
    
    [HttpPut("reference/{resumeId}")]
    [Authorize]
    public async Task<IActionResult> UpdateReferencelByResumeIdController(int resumeId, PostReferenceDTO  referenceDTO, IMapper mapper)
    {
        Reference  reference = mapper.Map<Reference>(referenceDTO);
        Reference updatedReference = await _repository.UpdateReferencesByResumeId(resumeId, reference);
            
        if(resumeId <= 0)
        {
            return NotFound("Invalid ID");
        }
        if(reference == null)
        {
            return NotFound("Invalid data");
        }
        if(updatedReference.Equals(reference))
        {
            return Ok("No changes detected");
        }
        if(updatedReference == null)
        {
            return NotFound("Reference not found");
        }
            
        var getReferenceDTO = mapper.Map<PostReferenceDTO>(updatedReference);

        var payload = new Payload<PostReferenceDTO>
        {
            Message = "Reference updated successfully.",
            Data = getReferenceDTO
        };
            
        return Ok(payload);
    }
    
    [HttpDelete("resume/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteResume(int id)
    {
        try
        {
            await _repository.DeleteResumeWithRelatedData(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
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
