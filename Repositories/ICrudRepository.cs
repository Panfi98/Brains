namespace BrainsToDo.Repositories;

public interface ICrudRepository<T>
{
    IEnumerable<T> GetAllEntities();
    
    T? GetEntityById(int id);
    
    T AddEntity(T entity);
    
    T? UpdateEntity(int id, T entity);
    
    bool DeleteEntity(int id);
}