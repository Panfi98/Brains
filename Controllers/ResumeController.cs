using AutoMapper;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainsToDo.Controllers;

    [ApiController]
    [Route("resume")]
    [Authorize]
    public class ResumeController(ResumeRepository repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCResumes(IMapper mapper)
        {
            var resumes = await repository.GetAllEntities();
            
            if (!resumes.Any())
            {
                return NotFound("No resume found");
            }

            var resumeDTOs = mapper.Map<IEnumerable<GetResumeDTO>>(resumes);

            var payload = new Payload<IEnumerable<GetResumeDTO>>()
            {
                Data = resumeDTOs,
            };
            
            return Ok(payload);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetResumeById(int id, IMapper mapper)
        {
            var resume = await repository.GetEntityById(id);
            var resumeDTO = mapper.Map<GetResumeDTO>(resume);
            
            if(id <= 0)
            {
                return NotFound("Invalid resume ID");
            }
            if(resume == null)
            {
                return NotFound("No resume found");
            }

            var payload = new Payload<GetResumeDTO>()
            {
                Data = resumeDTO
            };
            
            return Ok(payload);
        }

        [HttpPost]
        public async Task<ActionResult> CreateResume(PostResumeDTO resumeDTO, IMapper mapper)
        {
            if(resumeDTO == null)
            {
                return NotFound("Invalid request");
            }
            
            Resume resume = mapper.Map<Resume>(resumeDTO);
            resume.PersonId = resumeDTO.PersonId;
            resume.ResumeTemplateId = resumeDTO.ResumeTemplateId;
            
            var createdResume = await repository.AddEntity(resume);
            
            var getResumeDTO = mapper.Map<GetResumeDTO>(createdResume);

            var payload = new Payload<GetResumeDTO>()
            {
                Data = getResumeDTO
            };
            
            return CreatedAtAction(nameof(GetResumeById), new { id = createdResume.Id }, payload);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateResume(int id, PostResumeDTO resumeDTO, IMapper mapper)
        {
            Resume resume = mapper.Map<Resume>(resumeDTO);
            Resume updatedResume = await repository.UpdateEntity(id, resume);
            
            if(id <= 0)
            {
                return NotFound("Invalid resume ID");
            }
            if(resume == null)
            {
                return NotFound("Invalid resume data");
            }
            if(updatedResume.Equals(resume))
            {
                return Ok("No changes detected");
            }
            if(updatedResume == null)
            {
                return NotFound("No resume found");
            }
            
            var getResumeDTO = mapper.Map<GetResumeDTO>(updatedResume);

            var payload = new Payload<GetResumeDTO>()
            {
                Data = getResumeDTO
            };
            
            return Ok(payload);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResume(int id, IMapper mapper)
        {
            if(id <= 0)
            {
                return NotFound("Invalid resume ID");
            }
            
            var deletedResume = await repository.DeleteEntity(id);
            
            if(deletedResume == null)
            {
                return NotFound("No resume found");
            }
            
            var getResumeDTO = mapper.Map<GetContactDTO>(deletedResume);

            var payload = new Payload<GetContactDTO>()
            {
                Data = getResumeDTO
            };
            
            return Ok(payload);
        }
    }