using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class CompanyRepository (DataContext context) : ICrudRepository<Company>
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
            _context.Company.Add(entity);
            await _context.SaveChangesAsync();
             return entity;
        }

        public async Task<Company?> UpdateEntity(int id, Company entity)
        {
            var oldEntity = await _context.Company.FindAsync(id);
            
            if (oldEntity == null)
            {
                return null;
            }

            oldEntity.Name = entity.Name;
            oldEntity.Description = entity.Description;
            oldEntity.Address = entity.Address;
            oldEntity.Type = entity.Type;
            oldEntity.updatedAt = DateTime.UtcNow;
            
           await _context.SaveChangesAsync();
            return oldEntity;
        }

        public async Task<Company> DeleteEntity(int id)
        {
            var entity = await _context.Company.FindAsync(id);

            if (entity == null)
            {
                throw new KeyNotFoundException("Company not found");
            }

            _context.Company.Remove(entity);
          await  _context.SaveChangesAsync();
            
            return entity;
        }
    }
}