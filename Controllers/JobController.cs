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

public class JobController(ICrudRepository<Job> JobRepository, ICrudRepository<Company> CompanyRepository, IMapper mapper): ControllerBase
{
    private readonly ICrudRepository<Job> _repositoryJob = JobRepository;
    private readonly ICrudRepository<Company> _repositoryCompany = CompanyRepository;
    public readonly IMapper _mapper = mapper;
    
    [HttpGet]
    public IActionResult GetAllJobs()
    {
        var jobs = _repositoryJob.GetAllEntities().ToList();
        var jobsDTOs = _mapper.Map<List<GetJobDTO>>(jobs);
        
        foreach (var jobDTO in jobsDTOs)
        {
            if (jobDTO.CompanyId.HasValue)
            {
                var company = _repositoryCompany.GetEntityById(jobDTO.CompanyId.Value);
                
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

        var job = _repositoryJob.GetEntityById(id);

        if (job == null)
        {
            return NotFound("Job not found");
        }

        var jobDTO = _mapper.Map<GetJobDTO>(job);

        if (jobDTO.CompanyId.HasValue)
        {
            var company = _repositoryCompany.GetEntityById(jobDTO.CompanyId.Value);
            if (company != null)
            {
                jobDTO.Company = _mapper.Map<GetCompanyDTO>(company);
            }
        }

        return Ok(jobDTO);
    }
    
    [HttpPost]
    public IActionResult CreateJob(Job job)
    {
        if (job == null)
        {
            return BadRequest("Invalid job data");
        }

        job.createdAt = DateTime.UtcNow;
        job.updatedAt = DateTime.UtcNow;

        var createdJob = _repositoryJob.AddEntity(job);

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

        var GetJob = _repositoryJob.GetEntityById(id);

        if (GetJob == null)
        {
            return NotFound("Job not found");
        }
        
        _mapper.Map(job, GetJob);
        
        var updatedJob = _repositoryJob.UpdateEntity(id, job);
        
        return Ok(updatedJob);
    }
    
    [HttpDelete]
    public IActionResult DeletedJob(int id)
    {
        if (id <= 0)
        {
            return NotFound("Invalid job ID");
        }

        var deletedJob = _repositoryJob.DeleteEntity(id);

        if (deletedJob == false)
        {
            return NotFound("Job not found");
        }

        return NoContent();  
    }
    
}