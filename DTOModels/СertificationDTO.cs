using BrainsToDo.Models;

namespace BrainsToDo.DTOModels;

public class GetCertificationDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public DateTime ValidTo { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
    public bool SoftDeleted { get; set; }
    public Resume Resume {get; set;}
}

public class PostCertificationDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public DateTime ValidTo { get; set; }
    public int resumeId { get; set; }
}