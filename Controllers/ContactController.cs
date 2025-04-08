using AutoMapper;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using BrainsToDo.Helpers;
using BrainsToDo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainsToDo.Controllers;

    [ApiController]
    [Route("contact")]
    [Authorize]
    public class ContactController(ContactRepository repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllContacts(IMapper mapper)
        {
            var contacts = await repository.GetAllEntities();
            
            if (!contacts.Any())
            {
                return NotFound("No contact found");
            }

            var contactDTOs = mapper.Map<IEnumerable<GetContactDTO>>(contacts);

            var payload = new Payload<IEnumerable<GetContactDTO>>()
            {
                Data = contactDTOs
            };
            
            return Ok(payload);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetContactById(int id, IMapper mapper)
        {
            var contact = await repository.GetEntityById(id);
            var contactDTO = mapper.Map<GetContactDTO>(contact);
            
            if(id <= 0)
            {
                return NotFound("Invalid contact ID");
            }
            if(contact == null)
            {
                return NotFound("No contact found");
            }

            var payload = new Payload<GetContactDTO>()
            {
                Data = contactDTO
            };
            
            return Ok(payload);
        }

        [HttpPost]
        public async Task<ActionResult> CreateContact(PostContactDTO contactDTO, IMapper mapper)
        {
            if(contactDTO == null)
            {
                return NotFound("Invalid request");
            }
            
            Contact contact = mapper.Map<Contact>(contactDTO);
            contact.JobId = contactDTO.JobId;
            contact.CompanyId = contactDTO.CompanyId;
            
            var createdContact = await repository.AddEntity(contact);
            
            var getContactDTO = mapper.Map<GetContactDTO>(createdContact);

            var payload = new Payload<GetContactDTO>()
            {
                Data = getContactDTO
            };
            
            return CreatedAtAction(nameof(GetContactById), new { id = createdContact.Id }, payload);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateContact(int id, PostContactDTO contactDTO, IMapper mapper)
        {
            Contact contact = mapper.Map<Contact>(contactDTO);
            Contact updatedContact = await repository.UpdateEntity(id, contact);
            
            if(id <= 0)
            {
                return NotFound("Invalid contact ID");
            }
            if(contact == null)
            {
                return NotFound("Invalid contact data");
            }
            if(updatedContact.Equals(contact))
            {
                return Ok("No changes detected");
            }
            if(updatedContact == null)
            {
                return NotFound("No contact found");
            }
            
            var getContactDTO = mapper.Map<GetContactDTO>(updatedContact);

            var payload = new Payload<GetContactDTO>()
            {
                Data = getContactDTO
            };
            
            return Ok(payload);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id, IMapper mapper)
        {
            if(id <= 0)
            {
                return NotFound("Invalid contact ID");
            }
            
            var deletedJContact = await repository.DeleteEntity(id);
            
            if(deletedJContact == null)
            {
                return NotFound("No contact found");
            }
            
            var getContactDTO = mapper.Map<GetContactDTO>(deletedJContact);

            var payload = new Payload<GetContactDTO>()
            {
                Data = getContactDTO
            };
            
            return Ok(payload);
        }
    }