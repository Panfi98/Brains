using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class CompanyRepository (DataContext context) : ICrudRepository<Company>
    {
        private readonly DataContext _context = context;

        public IEnumerable<Company> GetAllEntities()
        {
            return _context.Company.ToList();
        }

        public Company? GetEntityById(int id)
        {
            return _context.Company.Find(id);
        }

        public Company AddEntity(Company entity)
        {
            _context.Company.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Company? UpdateEntity(int id, Company entity)
        {
            var oldEntity = _context.Company.Find(id);
            
            if (oldEntity == null)
            {
                return null;
            }

            oldEntity.Name = entity.Name;
            oldEntity.Description = entity.Description;
            oldEntity.Address = entity.Address;
            oldEntity.Type = entity.Type;
            oldEntity.updatedAt = DateTime.UtcNow;
            
            _context.SaveChanges();
            return oldEntity;
        }

        public bool DeleteEntity(int id)
        {
            var entity = _context.Company.Find(id);

            if (entity == null)
            {
                return false;
            }

            _context.Company.Remove(entity);
            _context.SaveChanges();
            
            return true;
        }
    }
}