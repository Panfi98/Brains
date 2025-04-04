using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories;

public class EducationRepository(DataContext context) : ICrudRepository<Education>
{
    private readonly DataContext _context = context;

    public async Task<IEnumerable<Education>> GetAllEntities()
    {
        return await _context.Education
            .Include(p => p.Person) 
            .ToListAsync();
    }

    public async Task<Education?> GetEntityById(int id)
    {
        return await _context.Education.Include(p => p.Person)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Education> AddEntity(Education entity)
    {
        _context.Education.Add(entity);
        await _context.SaveChangesAsync();
        return _context.Education.Include(p => p.Person).FirstOrDefault(p => p.Id == entity.Id);
    }

    public async Task<Education?> UpdateEntity(int id, Education entity)
    {
        var oldEntity = await _context.Education.FindAsync(id);
        if (oldEntity == null)
        {
            throw new KeyNotFoundException("Education not found");
        }

        oldEntity.Name = entity.Name;
        oldEntity.Type = entity.Type;
        oldEntity.StartDate = entity.StartDate;
        oldEntity.EndDate = entity.EndDate;
        oldEntity.Description = entity.Description;
        oldEntity.Degree = entity.Degree;
        oldEntity.Place = entity.Place;
        oldEntity.Active = entity.Active;
        //oldEntity.updatedAt = DateTime.Now;

        _context.Education.Update(oldEntity);
        await _context.SaveChangesAsync();
        return oldEntity;
    }

    public async Task<Education> DeleteEntity(int id)
    {
        var entity = await _context.Education.FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException("Education not found");
        };

        _context.Education.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}