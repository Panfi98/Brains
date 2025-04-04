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
[Route("company")]

public class CompanyController(ICrudRepository<Company> repository, IMapper mapper): ControllerBase
{
    private readonly ICrudRepository<Company> _repository = repository;
    public readonly IMapper _mapper = mapper;
    
    [HttpGet]
    public async Task<IActionResult> GetAllCompanies()
    {
        var companies = await _repository.GetAllEntities();
        var companiesDTOs = _mapper.Map<List<GetCompanyDTO>>(companies);
            
        if(!companies.Any()) 
        {
            return Ok (new List<GetCompanyDTO>());
        }
        
       return Ok(companiesDTOs);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyById(int id)
    {
        if (id <= 0)
        {
            return NotFound("Invalid company ID");
        }
        
        var company = await _repository.GetEntityById(id);

        if (company == null)
        {
            return NotFound("Company not found");
        }
        
        var companyDTO = _mapper.Map<GetCompanyDTO>(company);
        
        return Ok(companyDTO);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany(Company company)
    {
        if (company == null)
        {
            return BadRequest("Invalid company data");
        }
        
        var newCompany = _mapper.Map<Company>(company);
        
        var createdCompany = await _repository.AddEntity(newCompany);
        
        return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompany.Id }, createdCompany);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, Company company)
    {
        {
            if (id <= 0)
            {
                return NotFound("Invalid company ID");
            }

            if (company == null)
            {
                return BadRequest("Invalid company data");
            }
            
            var GetCompany = await  _repository.GetEntityById(id);
        
            if (GetCompany == null)
            {
                return NotFound ("Company not found");
            }
        
            _mapper.Map(company, GetCompany);
        
            var updatedCompany = await _repository.UpdateEntity(id, company);
        
            return Ok(updatedCompany);
        } 
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeletedCompany(int id)
    {
        if(id <= 0) 
        {
            return NotFound("Invalid company ID");
        }
        
        var deletedCompany = await _repository.DeleteEntity(id);

        if (deletedCompany == null)
        {
            return NotFound("Company not found");
        }
        
        return Ok(deletedCompany);
    }
    
}