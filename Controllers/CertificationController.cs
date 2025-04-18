using AutoMapper;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainsToDo.Controllers;

    [ApiController]
    [Route("certification")]
    [Authorize]
    public class CertificationController(CertificationRepository repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCertifications(IMapper mapper)
        {
            var certifications = await repository.GetAllEntities();
            
            if (!certifications.Any())
            {
                return NotFound("No certification found");
            }

            var certificationDTOs = mapper.Map<IEnumerable<GetCertificationDTO>>(certifications);

            var payload = new Payload<IEnumerable<GetCertificationDTO>>()
            {
                Data = certificationDTOs
            };
            
            return Ok(payload);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCertificationById(int id, IMapper mapper)
        {
            var certification = await repository.GetEntityById(id);
            var certificationDTO = mapper.Map<GetCertificationDTO>(certification);
            
            if(id <= 0)
            {
                return NotFound("Invalid certification ID");
            }
            if(certification == null)
            {
                return NotFound("No certification found");
            }

            var payload = new Payload<GetCertificationDTO>()
            {
                Data = certificationDTO
            };
            
            return Ok(payload);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCertification(PostCertificationDTO certificationDTO, IMapper mapper)
        {
            if(certificationDTO == null)
            {
                return NotFound("Invalid request");
            }
            
            Certification certification = mapper.Map<Certification>(certificationDTO);
            certification.ResumeId = certificationDTO.resumeId;
            
            var createdCertification = await repository.AddEntity(certification);
            
            var getCertificationDTO = mapper.Map<GetCertificationDTO>(createdCertification);

            var payload = new Payload<GetCertificationDTO>()
            {
                Data = getCertificationDTO
            };
            
            return CreatedAtAction(nameof(GetCertificationById), new { id = createdCertification.Id }, payload);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCertification(int id, PostCertificationDTO certificationDTO, IMapper mapper)
        {
            Certification certification = mapper.Map<Certification>(certificationDTO);
            Certification updatedCertification = await repository.UpdateEntity(id, certification);
            
            if(id <= 0)
            {
                return NotFound("Invalid certification ID");
            }
            if(certification == null)
            {
                return NotFound("Invalid certification data");
            }
            if(updatedCertification.Equals(certification))
            {
                return Ok("No changes detected");
            }
            if(updatedCertification == null)
            {
                return NotFound("No job found");
            }
            
            var getCertificationDTO = mapper.Map<GetCertificationDTO>(updatedCertification);

            var payload = new Payload<GetCertificationDTO>()
            {
                Data = getCertificationDTO
            };
            
            return Ok(payload);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> СertificationJob(int id, IMapper mapper)
        {
            if(id <= 0)
            {
                return NotFound("Invalid certification ID");
            }
            
            var certificationJob = await repository.DeleteEntity(id);
            
            if(certificationJob == null)
            {
                return NotFound("No certification found");
            }
            
            var getCertificationDTO = mapper.Map<GetCertificationDTO>(certificationJob);

            var payload = new Payload<GetCertificationDTO>()
            {
                Data = getCertificationDTO
            };
            
            return Ok(payload);
        }
    }