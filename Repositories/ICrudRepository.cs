using BrainsToDo.Models;

namespace BrainsToDo.Repositories;

public interface ICrudRepository<T>
{
    Task<IEnumerable<T>> GetAllEntities();
    
    Task<T?> GetEntityById(int id);
    
    Task<T> AddEntity(T entity);
    
    Task <T?> UpdateEntity(int id, T entity);
    
    Task<T?> DeleteEntity(int id);
}