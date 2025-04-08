using System.Security.Claims;
using System.Text;
using AutoMapper;
using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using BrainsToDo.Repositories.LoginLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BrainsToDo.Models;

    [ApiController]
    [Route("company")]
    public class CompanyController(CompanyRepository repository, IMapper mapper, IConfiguration configuration) : ControllerBase
    {
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetAllCompanies(IMapper mapper)
        {
            var companies = await repository.GetAllEntities();
            if(!companies.Any())
            {
                return NotFound("No company found");
            }
            var companiesDTOs = mapper.Map<IEnumerable<GetCompanyDTO>>(companies);

            var payload = new Payload<IEnumerable<GetCompanyDTO>>
            {
                Data = companiesDTOs
            };
            return Ok(payload);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetCompanyById(int id, IMapper mapper)
        {
            var company = await repository.GetEntityById(id);
            var companyDTO = mapper.Map<GetCompanyDTO>(company);
            
            if(id <= 0)
            {
                return NotFound("Invalid company ID");
            }
            if (company == null)
            {
                return NotFound("Company not found");
            }

            var payload = new Payload<GetCompanyDTO>
            {
                Data = companyDTO
            };
            
            return Ok(payload);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCompany(IMapper mapper, PostCompanyDTO companyDTO) 
        {
            if(companyDTO == null)
            {
                return NotFound("Invalid company data");
            };
            
            Company company = mapper.Map<Company>(companyDTO);
            var createdCompany = await repository.AddEntity(company);
            
            var getCompanyDTO = mapper.Map<GetCompanyDTO>(createdCompany);
            var payload = new Payload<GetCompanyDTO>
            {
                Data = getCompanyDTO
            };
            
            return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompany.Id }, payload);
        }
        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCompany(int id, PostCompanyDTO companyDTO, IMapper mapper)
        {
            Company company = mapper.Map<Company>(companyDTO);
            Company updatedCompany = await repository.UpdateEntity(id, company);
            
            if(id <= 0)
            {
                return NotFound("Invalid company ID");
            }
            if(company == null)
            {
                return NotFound("Invalid company data");
            }
            if(updatedCompany.Equals(company))
            {
                return Ok("No changes detected");
            }
            if(updatedCompany == null)
            {
                return NotFound("Company not found");
            }
            
            var getCompanyDTO = mapper.Map<GetCompanyDTO>(updatedCompany);

            var payload = new Payload<GetCompanyDTO>
            {
                Data = getCompanyDTO
            };
            
            return Ok(payload);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletedCompany(IMapper mapper, int id)
        {
            if(id <= 0)
            {
                return NotFound("Invalid company ID");
            }
            
            var deletedCompany = await repository.DeleteEntity(id);
            
            if (deletedCompany == null)
            {
                return NotFound("Company not found");
            }
            
            var getCompanyDTO = mapper.Map<GetCompanyDTO>(deletedCompany);

            var payload = new Payload<GetCompanyDTO>
            {
                Data = getCompanyDTO
            };
            
            return Ok(payload);
        }
    }