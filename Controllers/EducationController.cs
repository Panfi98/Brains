using AutoMapper;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BrainsToDo.Controllers;

    [ApiController]
    [Route("education")]
    public class EducationController(EducationRepository repository, IMapper mapper) : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetAllEducations(IMapper mapper)
        {
            var educations = await repository.GetAllEntities();
            if(!educations.Any())
            {
                return NotFound("No education found");
            }
            var educationDTOs = mapper.Map<IEnumerable<GetEducationDTO>>(educations);

            var payload = new Payload<IEnumerable<GetEducationDTO>>
            {
                Data = educationDTOs
            };
            return Ok(payload);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEducationById(int id, IMapper mapper)
        {
            var education = await repository.GetEntityById(id);
            var educationDTO = mapper.Map<GetEducationDTO>(education);
            
            if(id <= 0)
            {
                return NotFound("Invalid education ID");
            }
            if (education == null)
            {
                return NotFound("Education not found");
            }

            var payload = new Payload<GetEducationDTO>
            {
                Data = educationDTO
            };
            
            return Ok(payload);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser(IMapper mapper, PostEducationDTO educationDTO) 
        {
            if(educationDTO == null)
            {
                return NotFound("Invalid user data");
            };
            
            Education education = mapper.Map<Education>(educationDTO);
            education.PersonId = educationDTO.PersonId;
            
            var createdEducation = await repository.AddEntity(education);
            
            var getEducationDTO = mapper.Map<GetEducationDTO>(createdEducation);
            var payload = new Payload<GetEducationDTO>
            {
                Data = getEducationDTO
            };
            
            return CreatedAtAction(nameof(GetEducationById), new { id = createdEducation.Id }, payload);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEducation(int id, PostEducationDTO educationDTO, IMapper mapper)
        {
            Education education = mapper.Map<Education>(educationDTO);
            Education updatedEducation = await repository.UpdateEntity(id, education);
            
            if(id <= 0)
            {
                return NotFound("Invalid education ID");
            }
            if(education == null)
            {
                return NotFound("Invalid education data");
            }
            if(updatedEducation.Equals(education))
            {
                return Ok("No changes detected");
            }
            if(updatedEducation == null)
            {
                return NotFound("Education not found");
            }
            
            var getEducationDTO = mapper.Map<GetEducationDTO>(updatedEducation);

            var payload = new Payload<GetEducationDTO>
            {
                Data = getEducationDTO
            };
            
            return Ok(payload);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletedEducation(IMapper mapper, int id)
        {
            if(id <= 0)
            {
                return NotFound("Invalid education ID");
            }
            
            var deletedEducation = await repository.DeleteEntity(id);
            
            if (deletedEducation == null)
            {
                return NotFound("Education not found");
            }
            
            var getEducationDTO = mapper.Map<GetEducationDTO>(deletedEducation);

            var payload = new Payload<GetEducationDTO>
            {
                Data = getEducationDTO
            };
            
            return Ok(payload);
        }
    }