using BrainsToDo.Models;

namespace BrainsToDo.DTOModels;


public class ResumeMakerDTO
{
    public Resume Resume { get; set; }
    public List<Education> EducationList { get; set; }
    public List<Certification> Certifications { get; set; }
    public List<Project> Projects { get; set; }
    public List<Experience> ExperienceList { get; set; }
    public List<Skill> Skills { get; set; }
    public List<Reference> References { get; set; }
    public ResumeTemplate? ResumeTemplate { get; set; }
}
