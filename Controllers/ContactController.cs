using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Models;

[ApiController]
[Route("contact")]
[Authorize]
public class ContactController(ICrudRepository<Contact> ContactRepository, ICrudRepository<Job> JobRepository, ICrudRepository<Company> CompanyRepository, IMapper mapper): ControllerBase
{
    private readonly ICrudRepository<Contact> _repositoryContact = ContactRepository;
    private readonly ICrudRepository<Company> _repositoryCompany = CompanyRepository;
    private readonly ICrudRepository<Job> _repositoryJob = JobRepository;
    public readonly IMapper _mapper = mapper;
    
    [HttpGet]
    public async Task<IActionResult> GetAllJobs()
    {
        var contacts = await _repositoryContact.GetAllEntities();
    
        if (!contacts.Any())
        {
            return Ok(new List<GetContactDTO>());
        }

        var contactDTOs = _mapper.Map<List<GetContactDTO>>(contacts);
        
        foreach (var contactDTO in contactDTOs)
        {
            if (contactDTO.CompanyId.HasValue && contactDTO.JobId.HasValue)
            {
               
                var contact_company = await _repositoryCompany.GetEntityById(contactDTO.CompanyId.Value);
                var contact_job = await _repositoryJob.GetEntityById(contactDTO.JobId.Value);

                if (contact_company != null && contact_job != null)
                {
                   
                    contactDTO.Company = _mapper.Map<GetCompanyDTO>(contact_company);
                    contactDTO.Job = _mapper.Map<GetJobDTO>(contact_job);
                }
            }
        }
        
        return Ok(contactDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(int id)
    {
        var contact = await _repositoryContact.GetEntityById(id);
    
        if (contact == null)
        {
            return NotFound("Contact not found");
        }
        
        var contactDTO = _mapper.Map<GetContactDTO>(contact);
        
        if (contactDTO.CompanyId.HasValue && contactDTO.JobId.HasValue)
        {
            var contact_company = await _repositoryCompany.GetEntityById(contactDTO.CompanyId.Value);
            var contact_job = await _repositoryJob.GetEntityById(contactDTO.JobId.Value);
            
            if (contact_company != null && contact_job != null)
            {
                contactDTO.Company = _mapper.Map<GetCompanyDTO>(contact_company);
                contactDTO.Job = _mapper.Map<GetJobDTO>(contact_job);
            }
        }
        
        return Ok(contactDTO);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateContact(Contact contact)
    {
        if (contact == null)
        {
            return BadRequest("Invalid contact data");
        }

        contact.createdAt = DateTime.UtcNow;
        contact.updatedAt = DateTime.UtcNow;

        var createdContact = await _repositoryContact.AddEntity(contact);

        return CreatedAtAction(nameof(GetContactById), new { id = createdContact.Id }, createdContact);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact (int id, Contact contact)
    {
        if (id <= 0)
        {
            return NotFound("Invalid contact ID");
        }

        if (contact == null)
        {
            return BadRequest("Invalid contact data");
        }

        var GetContact =await _repositoryContact.GetEntityById(id);

        if (GetContact == null)
        {
            return NotFound("Contact not found");
        }
        
        _mapper.Map(contact, GetContact);
        
        var updatedContact = await _repositoryContact.UpdateEntity(id, contact);
        
        return Ok(updatedContact);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeletedContact(int id)
    {
        if (id <= 0)
        {
            return NotFound("Invalid contact ID");
        }

        var deletedContact = await _repositoryContact.DeleteEntity(id);

        if (deletedContact == null)
        {
            return NotFound("Contact not found");
        }

        return NoContent();  
    }
    
}