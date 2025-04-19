namespace BrainsToDo.Repositories;

public interface IResumeMaker<T, T1, T2, T3, T4, T5, T6>
{
    Task<T>  AddResume (T entity, int personId);
    Task<T1>  AddEducationList (T1 entity, int personId);
    Task<T2>  AddCertifications (T3 entity);
    Task<T3>  AddExperienceList (T3 entity);
    Task<T4>  AddProjects (T4 entity);
    Task<T5>  AddSkills (T5 entity);
    Task<T6>  AddReferences (T6 entity);
}