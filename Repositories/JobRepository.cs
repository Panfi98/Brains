using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class JobRepository (DataContext context) : ICrudRepository<Job>
    {
        private readonly DataContext _context = context;

        public IEnumerable<Job> GetAllEntities()
        {
            return _context.Job.Include(j => j.Company).ToList();
        }

        public Job GetEntityById(int id)
        {
            return _context.Job.Include(j => j.Company).FirstOrDefault(j => j.Id == id);
        }

        public Job AddEntity(Job entity)
        {
            _context.Job.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Job UpdateEntity(int id, Job entity)
        {
            var oldEntity = _context.Job.Find(id);
            
            if (oldEntity == null)
            {
                return null;
            }

            oldEntity.Name = entity.Name;
            oldEntity.Description = entity.Description;
            oldEntity.Place = entity.Place;
            oldEntity.Position = entity.Position;
            oldEntity.updatedAt = DateTime.UtcNow;
            
            _context.SaveChanges();
            return oldEntity;
        }

        public bool DeleteEntity(int id)
        {
            var entity = _context.Job.Find(id);

            if (entity == null)
            {
                return false;
            }

            _context.Job.Remove(entity);
            _context.SaveChanges();
            
            return true;
        }
    }
}