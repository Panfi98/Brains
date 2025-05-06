using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class UserRepository(DataContext context) : ICrudRepository<User>
    {
        private readonly DataContext _context = context;
        
        public async Task<bool> UserExists(string username, string email)
        {
            return await _context.User.AnyAsync(u => u.Name == username || u.Email == email);
        }
        
        public async Task<IEnumerable<User>> GetAllEntities()
        {
            return await _context.User.ToListAsync();    
        }

        public async Task<User?> GetEntityById(int id)
        {
            return await _context.User.FindAsync(id);
        }
        
        public async Task<User> AddEntity(User entity)
        {
            _context.User.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
            
        }

        public async Task<User?> UpdateEntity(int id, User entity)
        {
            var oldEntity = await _context.User.FindAsync(id);
            if(oldEntity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            };
            
            oldEntity.Name = entity.Name;
            oldEntity.Password = entity.Password;
            oldEntity.updatedAt = DateTime.UtcNow;
            
            _context.User.Update(oldEntity);
            await _context.SaveChangesAsync();
            return oldEntity;
        }

        public async Task<User?> DeleteEntity(int id)
        {
            var entity = await _context.User.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }
            
            _context.User.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
    
    
}