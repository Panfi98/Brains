namespace BrainsToDo.DTOModels;

public class CertificationDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public DateTime ValidTo { get; set; }
}