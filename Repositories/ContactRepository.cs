using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class ContactRepository(DataContext context) : ICrudRepository<Contact>
    {
        private readonly DataContext _context = context;

        public async Task<IEnumerable<Contact>> GetAllEntities()
        {
            return await _context.Contact
                .Include(c => c.Job)
                .Include(c => c.Company )
                .ToListAsync();
        }

        public async Task<Contact?> GetEntityById(int id)
        {
            return await _context.Contact.Include(c => c.Job)
                .Include(c => c.Company)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Contact> AddEntity(Contact entity)
        {   
            if (entity.CompanyId == 0)
            {
                entity.CompanyId = null;
            }
            
            if (entity.JobId == 0)
            {
                entity.JobId = null;
            }
            entity.createdAt = DateTime.UtcNow;
            entity.updatedAt = DateTime.UtcNow;
            
            _context.Contact.Add(entity);
            await _context.SaveChangesAsync();
            
            return  await _context.Contact.Include(c => c.Job)
                .Include(c => c.Company)
                .FirstOrDefaultAsync(c => c.Id == entity.Id);
        }

        public async Task<Contact?> UpdateEntity(int id, Contact entity)
        {
            var oldEntity = await _context.Contact.FindAsync(id);
            if (oldEntity == null)
            {
                throw new KeyNotFoundException("Contact not found");
            }

            oldEntity.Name = entity.Name;
            oldEntity.PhoneNumber = entity.PhoneNumber;
            oldEntity.Email = entity.Email;
            oldEntity.updatedAt = DateTime.UtcNow;

            if (entity.CompanyId != 0)
            {
                oldEntity.CompanyId = entity.CompanyId;
            }
            else
            {
                oldEntity.CompanyId = null;
            }
            
            if (entity.JobId != 0)
            {
                oldEntity.JobId = entity.JobId;
            }
            else
            {
                oldEntity.JobId = null;
            }
            
            
            _context.Contact.Update(oldEntity);
            await _context.SaveChangesAsync();
            return oldEntity;
        }

        public async Task<Contact> DeleteEntity(int id)
        {
            var entity = await _context.Contact.FindAsync(id);
           
            if (entity == null)
            {
                throw new KeyNotFoundException("Contact not found");
            };
            
            entity.deletedAt = DateTime.UtcNow;
            entity.updatedAt = DateTime.UtcNow;
            entity.SoftDeleted = true;
            
            _context.Contact.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}