using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class PersonRepository(DataContext context) : ICrudRepository<Person>
    {
        private readonly DataContext _context = context;

        public async Task<IEnumerable<Person>> GetAllEntities()
        {
            return await _context.Person
                .Include(p => p.User) 
                .ToListAsync();
        }

        public async Task<Person?> GetEntityById(int id)
        {
            return await _context.Person.Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Person> AddEntity(Person entity)
        {
            _context.Person.Add(entity);
            await _context.SaveChangesAsync();
            return _context.Person.Include(p => p.User).FirstOrDefault(p => p.Id == entity.Id);
        }

        public async Task<Person?> UpdateEntity(int id, Person entity)
        {
            var oldEntity = await _context.Person.FindAsync(id);
            if (oldEntity == null)
            {
                throw new KeyNotFoundException("Person not found");
            }

            oldEntity.FirstName = entity.FirstName;
            oldEntity.LastName = entity.LastName;
            oldEntity.Email = entity.Email;
            oldEntity.PhoneNumber = entity.PhoneNumber;
            oldEntity.Address = entity.Address;
            oldEntity.BirthDate = entity.BirthDate;
            oldEntity.PictureURL = entity.PictureURL;
            //oldEntity.updatedAt = DateTime.Now;

            _context.Person.Update(oldEntity);
            await _context.SaveChangesAsync();
            return oldEntity;
        }

        public async Task<Person> DeleteEntity(int id)
        {
            var entity = await _context.Person.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Person not found");
            };

            _context.Person.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}