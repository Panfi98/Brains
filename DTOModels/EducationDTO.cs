namespace BrainsToDo.DTOModels;

public class EducationDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Degree { get; set; }
    public string Category { get; set; }
    public string Place { get; set; }
    public bool Active { get; set; }
}