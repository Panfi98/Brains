namespace BrainsToDo.Repositories;

public interface ICrudRepository<T>
{
    IEnumerable<T> GetAllTasks();
    
    T? GetTaskById(int id);
    
    T AddTask(T task);
    
    T? UpdateTask(int id, T task);
    
    bool DeleteTask(int id);
}