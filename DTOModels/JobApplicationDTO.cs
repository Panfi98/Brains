namespace BrainsToDo.DTOModels;

public class JobApplicationDTO
{
    public string Name { get; set; }
    public string Status { get; set; }
    public DateTime ApliedAt { get; set; }
    public string Description { get; set; }
}