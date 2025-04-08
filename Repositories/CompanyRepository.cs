using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class CompanyRepository(DataContext context) : ICrudRepository<Company>
    {
        private readonly DataContext _context = context;
        
        public async Task<IEnumerable<Company>> GetAllEntities()
        {
            return await _context.Company.ToListAsync();    
        }

        public async Task<Company?> GetEntityById(int id)
        {
            return await _context.Company.FindAsync(id);
        }
        
        public async Task<Company> AddEntity(Company entity)
        {
            entity.updatedAt = DateTime.UtcNow;
            entity.createdAt = DateTime.UtcNow;
            
            _context.Company.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Company?> UpdateEntity(int id, Company entity)
        {
            var oldEntity = await _context.Company.FindAsync(id);
            if(oldEntity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            };
            
            oldEntity.Name = entity.Name;
            oldEntity.Description = entity.Description;
            oldEntity.Address = entity.Address;
            oldEntity.Type = entity.Type;
            oldEntity.updatedAt = DateTime.UtcNow;
            
            _context.Company.Update(oldEntity);
            await _context.SaveChangesAsync();
            return oldEntity;
        }

        public async Task<Company?> DeleteEntity(int id)
        {
            var entity = await _context.Company.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }
            
            entity.deletedAt = DateTime.UtcNow;
            entity.updatedAt = DateTime.UtcNow;
            entity.SoftDeleted = true;
            
            _context.Company.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}