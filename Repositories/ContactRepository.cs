using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class ContactRepository (DataContext context) : ICrudRepository<Contact>
    {
        private readonly DataContext _context = context;

        public IEnumerable<Contact> GetAllEntities()
        {
            return _context.Contact
                .Include(c => c.Company)
                .Include(c => c.Job)
                .ToList();
            
        }

        public Contact GetEntityById(int id)
        {
            return _context.Contact.Include(c => c.Company).Include(c => c.Job).FirstOrDefault(c => c.Id == id);
      
        }

        public Contact AddEntity(Contact entity)
        {
            _context.Contact.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Contact UpdateEntity(int id, Contact entity)
        {
            var oldEntity = _context.Contact.Find(id);
            
            if (oldEntity == null)
            {
                return null;
            }

            oldEntity.Name = entity.Name;
            oldEntity.Email = entity.Email;
            oldEntity.PhoneNumber = entity.PhoneNumber;
            oldEntity.updatedAt = DateTime.UtcNow;
            
            _context.SaveChanges();
            return oldEntity;
        }

        public bool DeleteEntity(int id)
        {
            var entity = _context.Contact.Find(id);

            if (entity == null)
            {
                return false;
            }

            _context.Contact.Remove(entity);
            _context.SaveChanges();
            
            return true;
        }
    }
}