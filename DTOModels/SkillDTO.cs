using BrainsToDo.Models;

namespace BrainsToDo.DTOModels;

public class SkillDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int Level { get; set; }
}

public class PostSkillDTOForResumeMaker
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int Level { get; set; }
    public Status Status { get; set; }
    public int ResumeId { get; set; }
}