using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class ResumeRepository(DataContext context) : ICrudRepository<Resume>
    {
        private readonly DataContext _context = context;

        public async Task<IEnumerable<Resume>> GetAllEntities()
        {
            return await _context.Resume
                .Include(r => r.Person)
                .Include(r => r.ResumeTemplate )
                .ToListAsync();
        }

        public async Task<Resume?> GetEntityById(int id)
        {
            return await _context.Resume.Include(r => r.Person)
                .Include(r => r.ResumeTemplate)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Resume> AddEntity(Resume entity)
        {   
            if (entity.PersonId == 0)
            {
                entity.PersonId = null;
            }
            
            if (entity.ResumeTemplateId == 0)
            {
                entity.ResumeTemplateId = null;
            }
            entity.createdAt = DateTime.UtcNow;
            entity.updatedAt = DateTime.UtcNow;
            
            _context.Resume.Add(entity);
            await _context.SaveChangesAsync();
            
            return  await _context.Resume.Include(r => r.Person)
                .Include(r => r.ResumeTemplate)
                .FirstOrDefaultAsync(r => r.Id == entity.Id);
        }

        public async Task<Resume?> UpdateEntity(int id, Resume entity)
        {
            var oldEntity = await _context.Resume.FindAsync(id);
            if (oldEntity == null)
            {
                throw new KeyNotFoundException("Resume not found");
            }

            oldEntity.FirstName = entity.FirstName;
            oldEntity.LastName = entity.LastName;
            oldEntity.Email = entity.Email;
            oldEntity.PhoneNumber = entity.PhoneNumber;
            oldEntity.Address = entity.Address;
            oldEntity.PictureURL = entity.PictureURL;
            oldEntity.updatedAt = DateTime.UtcNow;

            if (entity.PersonId != 0)
            {
                oldEntity.PersonId = entity.PersonId;
            }
            else
            {
                oldEntity.PersonId = null;
            }
            
            if (entity.ResumeTemplateId != 0)
            {
                oldEntity.ResumeTemplateId = entity.ResumeTemplateId;
            }
            else
            {
                oldEntity.ResumeTemplateId = null;
            }
            
            
            _context.Resume.Update(oldEntity);
            await _context.SaveChangesAsync();
            return oldEntity;
        }

        public async Task<Resume> DeleteEntity(int id)
        {
            var entity = await _context.Resume.FindAsync(id);
           
            if (entity == null)
            {
                throw new KeyNotFoundException("Resume not found");
            };
            
            entity.deletedAt = DateTime.UtcNow;
            entity.updatedAt = DateTime.UtcNow;
            entity.SoftDeleted = true;
            
            _context.Resume.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}