namespace BrainsToDo.Repositories;

public interface IResumeMaker<T>
{
    Task<T>  AddResume (T entity, int Id);
    Task<T>  AddResumeTemplate (T entity);
    Task<T>  AddEducationList (T entity, int Id);
    Task<T>  AddCertifications (T entity);
    Task<T>  AddExperienceList (T entity);
    Task<T>  AddProjects (T entity);
    Task<T>  AddSkills (T entity);
    Task<T>  AddReferences (T entity);
}