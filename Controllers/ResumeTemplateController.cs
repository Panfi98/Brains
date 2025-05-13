using AutoMapper;
using BrainsToDo.DTOModels;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BrainsToDo.Models;

    [ApiController]
    [Route("resumetemplate")]
    public class ResumeTemplateController(ResumeTemplateRepository repository, IMapper mapper, IConfiguration configuration) : ControllerBase
    {
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetAllResumeTemplates(IMapper mapper)
        {
            var resumeTemplates = await repository.GetAllEntities();
            if(!resumeTemplates.Any())
            {
                return NotFound("No resume template found");
            }
            var resumeTemplateDTOs = mapper.Map<IEnumerable<GetResumeTemplateDTO>>(resumeTemplates);

            var payload = new Payload<IEnumerable<GetResumeTemplateDTO>>
            {
                Data = resumeTemplateDTOs
            };
            return Ok(payload);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetResumeTemplateById(int id, IMapper mapper)
        {
            var resumeTemplate = await repository.GetEntityById(id);
            var resumeTemplateDTO = mapper.Map<GetResumeTemplateDTO>(resumeTemplate);
            
            if(id <= 0)
            {
                return NotFound("Invalid resume template ID");
            }
            if (resumeTemplate == null)
            {
                return NotFound("Resume template not found");
            }

            var payload = new Payload<GetResumeTemplateDTO>
            {
                Data = resumeTemplateDTO
            };
            
            return Ok(payload);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateResumeTemplate(IMapper mapper, PostResumeTemplateDTO resumeTemplateDTO) 
        {
            if(resumeTemplateDTO == null)
            {
                return NotFound("Invalid resume template data");
            };
            
            ResumeTemplate resumeTemplate = mapper.Map<ResumeTemplate>(resumeTemplateDTO);
            var createdResumeTemplate = await repository.AddEntity(resumeTemplate);
            
            var getResumeTemplateDTO = mapper.Map<GetResumeTemplateDTO>(createdResumeTemplate);
            var payload = new Payload<GetResumeTemplateDTO>
            {
                Data = getResumeTemplateDTO
            };
            
            return CreatedAtAction(nameof(GetResumeTemplateById), new { id = createdResumeTemplate.Id }, payload);
        }
        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateResumeTemplate(int id, PostResumeTemplateDTO resumeTemplateDTO, IMapper mapper)
        {
            ResumeTemplate resumeTemplate = mapper.Map<ResumeTemplate>(resumeTemplateDTO);
            ResumeTemplate updatedResumeTemplate= await repository.UpdateEntity(id, resumeTemplate);
            
            if(id <= 0)
            {
                return NotFound("Invalid resume template ID");
            }
            if(resumeTemplate == null)
            {
                return NotFound("Invalid resume template data");
            }
            if(updatedResumeTemplate.Equals(resumeTemplate))
            {
                return Ok("No changes detected");
            }
            if(updatedResumeTemplate == null)
            {
                return NotFound("Resume template not found");
            }
            
            var getResumeTemplateDTO = mapper.Map<GetResumeTemplateDTO>(updatedResumeTemplate);

            var payload = new Payload<GetResumeTemplateDTO>
            {
                Data = getResumeTemplateDTO
            };
            
            return Ok(payload);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletedResumeTemplate(IMapper mapper, int id)
        {
            if(id <= 0)
            {
                return NotFound("Invalid resume template ID");
            }
            
            var deletedResumeTemplate = await repository.DeleteEntity(id);
            
            if (deletedResumeTemplate == null)
            {
                return NotFound("Resume template not found");
            }
            
            var getResumeTemplateDTO = mapper.Map<GetResumeTemplateDTO>(deletedResumeTemplate);

            var payload = new Payload<GetResumeTemplateDTO>
            {
                Data = getResumeTemplateDTO
            };
            
            return Ok(payload);
        }
    }