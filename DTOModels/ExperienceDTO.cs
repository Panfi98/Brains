using BrainsToDo.Models;

namespace BrainsToDo.DTOModels;

public class ExperienceDTO
{
    public string Name { get; set; }
    public string Organisation { get; set; }
    public string Type { get; set; }
    public string Position { get; set; }
    public string Description { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public bool Active { get; set; }
    
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
    public bool SoftDeleted { get; set; }

}
