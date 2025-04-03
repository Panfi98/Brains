using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Models;

[ApiController]
[Route("job")]

public class JobController(ICrudRepository<Job> repository, IMapper mapper): ControllerBase
{
    private readonly ICrudRepository<Job> _repository = repository;
    public readonly IMapper _mapper = mapper;
    private readonly DataContext _context;

    [HttpGet]
    public IActionResult GetAllJobs()
    {
        var jobs = _repository.GetAllEntities().ToList();
        var jobsDTOs = _mapper.Map<List<GetJobDTO>>(jobs);

        foreach (var jobDTO in jobsDTOs)
        {
            if (jobDTO.CompanyId.HasValue)
            {
                var company = _context.Company.Find(jobDTO.CompanyId.Value);
            
                if (company != null)
                {
                    jobDTO.Company = _mapper.Map<GetCompanyDTO>(company);
                }
            }
        }

        if (!jobs.Any())
        {
            return Ok(new List<GetJobDTO>());
        }

        return Ok(jobsDTOs);
    }

    [HttpGet("{id}")]
    public IActionResult GetJobById(int id)
    {
        if (id <= 0)
        {
            return NotFound("Invalid job ID");
        }

        var job = _repository.GetEntityById(id);

        if (job == null)
        {
            return NotFound("Job not found");
        }

        var jobDTO = _mapper.Map<GetJobDTO>(job);
        
        if (jobDTO.CompanyId.HasValue)
        {
            var company = _context.Company.Find(jobDTO.CompanyId.Value);
        
            if (company != null)
            {
                jobDTO.Company = _mapper.Map<GetCompanyDTO>(company);
            }
        }

        return Ok(jobDTO);
    }

    [HttpPost]
    public IActionResult CreateJobAndCompany(Job job)
    {
        
        if (job == null)
        {
            return BadRequest("Invalid job data");
        }
        
        job.createdAt = DateTime.UtcNow;
        job.updatedAt = DateTime.UtcNow;
        
        var createdJob = _repository.AddEntity(job);

        return CreatedAtAction(nameof(GetJobById), new { id = createdJob.Id }, createdJob);
    }
    
   
    
    [HttpPut("{id}")]
    public IActionResult UpdateJob(int id, Job job)
    {
       
        if (id <= 0)
        {
            return NotFound("Invalid job ID");
        }

        if (job == null)
        {
            return BadRequest("Invalid job data");
        }

        var existingJob = _repository.GetEntityById(id);

        if (existingJob == null)
        {
            return NotFound("Job not found");
        }
        
        existingJob.Name = job.Name;
        existingJob.Description = job.Description;
        existingJob.Place = job.Place;
        existingJob.Position = job.Position;
        existingJob.CompanyId = job.CompanyId;  
        existingJob.updatedAt = DateTime.UtcNow;
       
        var updatedJob = _repository.UpdateEntity(id, existingJob);

        return Ok(updatedJob);
    }
    
    [HttpDelete]
    public IActionResult DeletedJob(int id)
    {
        if (id <= 0)
        {
            return NotFound("Invalid job ID");
        }

        var deletedJob = _repository.DeleteEntity(id);

        if (deletedJob == false)
        {
            return NotFound("Job not found");
        }

        return NoContent();  
    }
    
}