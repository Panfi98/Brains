using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class CertificationRepository(DataContext context) : ICrudRepository<Certification>
    {
        private readonly DataContext _context = context;

        public async Task<IEnumerable<Certification>> GetAllEntities()
        {
            return await _context.Certification
                .Include(c => c.Resume) 
                .ToListAsync();
        }

        public async Task<Certification?> GetEntityById(int id)
        {
            return await _context.Certification.Include(c => c.Resume)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Certification> AddEntity(Certification entity)
        {   
            if (entity.ResumeId == 0)
            {
                entity.ResumeId = null;
            }
            entity.createdAt = DateTime.UtcNow;
            entity.updatedAt = DateTime.UtcNow;
            
            _context.Certification.Add(entity);
            await _context.SaveChangesAsync();
            return _context.Certification.Include(c => c.Resume).FirstOrDefault(c => c.Id == entity.Id);
        }

        public async Task<Certification?> UpdateEntity(int id, Certification entity)
        {
            var oldEntity = await _context.Certification.FindAsync(id);
            if (oldEntity == null)
            {
                throw new KeyNotFoundException("Certification not found");
            }

            oldEntity.Name = entity.Name;
            oldEntity.Description = entity.Description;
            oldEntity.Date = entity.Date;
            oldEntity.Url = entity.Url;
            oldEntity.Type = entity.Type;
            oldEntity.ValidTo = entity.ValidTo;
           
            oldEntity.updatedAt = DateTime.UtcNow;

            if (entity.ResumeId != 0)
            {
                oldEntity.ResumeId = entity.ResumeId;
            }
            else
            {
                oldEntity.ResumeId = null;
            }
            
            
            _context.Certification.Update(oldEntity);
            await _context.SaveChangesAsync();
            return oldEntity;
        }

        public async Task<Certification> DeleteEntity(int id)
        {
            var entity = await _context.Certification.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Certification not found");
            };
            
            entity.deletedAt = DateTime.UtcNow;
            entity.updatedAt = DateTime.UtcNow;
            entity.SoftDeleted = true;
            
            _context.Certification.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}