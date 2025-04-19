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
