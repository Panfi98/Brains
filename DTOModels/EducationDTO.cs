namespace BrainsToDo.DTOModels;

public class EducationDTO
{
    public string Title { get; set; }
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public string Degree { get; set; }
    public string Place { get; set; }
    public bool Active { get; set; }
}