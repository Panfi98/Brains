using BrainsToDo.Models;

namespace BrainsToDo.DTOModels;

public class ProjectDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Completed { get; set; }
}

public class PostProjectDTOForResumeMaker
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Completed { get; set; }
    public Status Status { get; set; }
    public int ResumeId { get; set; }
    
}