using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class ResumeTemplateRepository(DataContext context) : ICrudRepository<ResumeTemplate>
    {
        private readonly DataContext _context = context;
        
        public async Task<IEnumerable<ResumeTemplate>> GetAllEntities()
        {
            return await _context.ResumeTemplate.ToListAsync();    
        }

        public async Task<ResumeTemplate?> GetEntityById(int id)
        {
            return await _context.ResumeTemplate.FindAsync(id);
        }
        
        public async Task<ResumeTemplate> AddEntity(ResumeTemplate entity)
        {
            entity.updatedAt = DateTime.UtcNow;
            entity.createdAt = DateTime.UtcNow;
            
            _context.ResumeTemplate.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ResumeTemplate?> UpdateEntity(int id, ResumeTemplate entity)
        {
            var oldEntity = await _context.ResumeTemplate.FindAsync(id);
            if(oldEntity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            };
            
            oldEntity.Name = entity.Name;
            oldEntity.Order = entity.Order;
            oldEntity.CategoriesIncluded = entity.CategoriesIncluded;
            oldEntity.updatedAt = DateTime.UtcNow;
            
            _context.ResumeTemplate.Update(oldEntity);
            await _context.SaveChangesAsync();
            return oldEntity;
        }

        public async Task<ResumeTemplate?> DeleteEntity(int id)
        {
            var entity = await _context.ResumeTemplate.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }
            
            entity.deletedAt = DateTime.UtcNow;
            entity.updatedAt = DateTime.UtcNow;
            entity.SoftDeleted = true;
            
            _context.ResumeTemplate.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}