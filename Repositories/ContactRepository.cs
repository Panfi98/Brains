using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class ContactRepository (DataContext context) : ICrudRepository<Contact>
    {
        private readonly DataContext _context = context;

        public async Task<IEnumerable<Contact>> GetAllEntities()
        {
            return await _context.Contact
                .Include(c => c.Company)
                .Include(c => c.Job)
                .ToListAsync();
            
        }

        public async Task<Contact> GetEntityById(int id)
        {
            return await _context.Contact.Include(c => c.Company).Include(c => c.Job).FirstOrDefaultAsync(c => c.Id == id);
      
        }

        public async Task<Contact> AddEntity(Contact entity)
        {
            _context.Contact.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Contact> UpdateEntity(int id, Contact entity)
        {
            var oldEntity = await _context.Contact.FindAsync(id);
            
            if (oldEntity == null)
            {
                return null;
            }

            oldEntity.Name = entity.Name;
            oldEntity.Email = entity.Email;
            oldEntity.PhoneNumber = entity.PhoneNumber;
            oldEntity.updatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return oldEntity;
        }

        public async Task<Contact> DeleteEntity(int id)
        {
            var entity = await _context.Contact.FindAsync(id);

            if (entity == null)
            {
                throw new KeyNotFoundException("Contact not found");
            }

            _context.Contact.Remove(entity);
            await _context.SaveChangesAsync();
            
            return entity;
        }
    }
}