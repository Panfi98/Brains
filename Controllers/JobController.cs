using AutoMapper;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainsToDo.Controllers;

    [ApiController]
    [Route("job")]
    [Authorize]
    public class JobController(JobRepository repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllJobs(IMapper mapper)
        {
            var jobs = await repository.GetAllEntities();
            
            if (!jobs.Any())
            {
                return NotFound("No job found");
            }

            var jobDTOs = mapper.Map<IEnumerable<GetJobDTO>>(jobs);

            var payload = new Payload<IEnumerable<GetJobDTO>>()
            {
                Data = jobDTOs
            };
            
            return Ok(payload);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetJobById(int id, IMapper mapper)
        {
            var job = await repository.GetEntityById(id);
            var jobDTO = mapper.Map<GetJobDTO>(job);
            
            if(id <= 0)
            {
                return NotFound("Invalid company ID");
            }
            if(job == null)
            {
                return NotFound("No job found");
            }

            var payload = new Payload<GetJobDTO>()
            {
                Data = jobDTO
            };
            
            return Ok(payload);
        }

        [HttpPost]
        public async Task<ActionResult> CreateJob(PostJobDTO jobDTO, IMapper mapper)
        {
            if(jobDTO == null)
            {
                return NotFound("Invalid request");
            }
            
            Job job = mapper.Map<Job>(jobDTO);
            job.CompanyId = jobDTO.CompanyId;
            
            var createdJob = await repository.AddEntity(job);
            
            var getJobDTO = mapper.Map<GetJobDTO>(createdJob);

            var payload = new Payload<GetJobDTO>()
            {
                Data = getJobDTO
            };
            
            return CreatedAtAction(nameof(GetJobById), new { id = createdJob.Id }, payload);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateJob(int id, PostJobDTO jobDTO, IMapper mapper)
        {
            Job job = mapper.Map<Job>(jobDTO);
            Job updatedJob = await repository.UpdateEntity(id, job);
            
            if(id <= 0)
            {
                return NotFound("Invalid job ID");
            }
            if(job == null)
            {
                return NotFound("Invalid job data");
            }
            if(updatedJob.Equals(job))
            {
                return Ok("No changes detected");
            }
            if(updatedJob == null)
            {
                return NotFound("No job found");
            }
            
            var getJobDTO = mapper.Map<GetJobDTO>(updatedJob);

            var payload = new Payload<GetJobDTO>()
            {
                Data = getJobDTO
            };
            
            return Ok(payload);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id, IMapper mapper)
        {
            if(id <= 0)
            {
                return NotFound("Invalid job ID");
            }
            
            var deletedJob = await repository.DeleteEntity(id);
            
            if(deletedJob == null)
            {
                return NotFound("No job found");
            }
            
            var getJobDTO = mapper.Map<GetJobDTO>(deletedJob);

            var payload = new Payload<GetJobDTO>()
            {
                Data = getJobDTO
            };
            
            return Ok(payload);
        }
    }