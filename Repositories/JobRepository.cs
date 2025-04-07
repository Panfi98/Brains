using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class JobRepository(DataContext context) : ICrudRepository<Job>
    {
        private readonly DataContext _context = context;

        public async Task<IEnumerable<Job>> GetAllEntities()
        {
            return await _context.Job
                .Include(j => j.Company) 
                .ToListAsync();
        }

        public async Task<Job?> GetEntityById(int id)
        {
            return await _context.Job.Include(j => j.Company)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<Job> AddEntity(Job entity)
        {
            _context.Job.Add(entity);
            await _context.SaveChangesAsync();
            return _context.Job.Include(j => j.Company).FirstOrDefault(j => j.Id == entity.Id);
        }

        public async Task<Job?> UpdateEntity(int id, Job entity)
        {
            var oldEntity = await _context.Job.FindAsync(id);
            if (oldEntity == null)
            {
                throw new KeyNotFoundException("Job not found");
            }

            oldEntity.Name = entity.Name;
            oldEntity.Description = entity.Description;
            oldEntity.Place = entity.Place;
            oldEntity.Position = entity.Position;
            oldEntity.updatedAt = DateTime.UtcNow;

            _context.Job.Update(oldEntity);
            await _context.SaveChangesAsync();
            return oldEntity;
        }

        public async Task<Job> DeleteEntity(int id)
        {
            var entity = await _context.Job.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Job not found");
            };

            _context.Job.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}