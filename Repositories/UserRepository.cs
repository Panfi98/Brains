using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class UserRepository(DataContext context) : ICrudRepository<User>
    {
        private readonly DataContext _context = context;
        
        public IEnumerable<User> GetAllEntities()
        {
            return _context.Users.ToList();    
        }

        public User? GetEntityById(int id)
        {
            return _context.Users.Find(id);
        }
        
        public User AddEntity(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public User? UpdateEntity(int id, User entity)
        {
            var oldEntity = _context.Users.Find(id);
            if(oldEntity == null) return null;
            
            oldEntity.Name = entity.Name;
            oldEntity.Password = entity.Password;
            oldEntity.updatedAt = DateTime.Now;
            
            _context.SaveChanges();
            return oldEntity;
        }

        public bool DeleteEntity(int id)
        {
            var entity = _context.Users.Find(id);
            if(entity == null) return false;
            
            _context.Users.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}