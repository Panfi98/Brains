using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories
{
    public class PersonRepository(DataContext context) : ICrudRepository<Person>
    {
        private readonly DataContext _context = context;

        public IEnumerable<Person> GetAllEntities()
        {
            return _context.Persons.ToList();
        }

        public Person? GetEntityById(int id)
        {
            return _context.Persons.Find(id);
        }

        public Person AddEntity(Person entity)
        {
            _context.Persons.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Person? UpdateEntity(int id, Person entity)
        {
            var oldEntity = _context.Persons.Find(id);
            if (oldEntity == null) return null;

            oldEntity.FirstName = entity.FirstName;
            oldEntity.LastName = entity.LastName;
            oldEntity.Email = entity.Email;
            oldEntity.PhoneNumber = entity.PhoneNumber;
            oldEntity.Address = entity.Address;
            oldEntity.BirthDate = entity.BirthDate;
            oldEntity.PictureURL = entity.PictureURL;
            oldEntity.updatedAt = DateTime.Now;

            _context.SaveChanges();
            return oldEntity;
        }

        public bool DeleteEntity(int id)
        {
            var entity = _context.Persons.Find(id);
            if (entity == null) return false;

            _context.Persons.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}