namespace BrainsToDo.DTOModels;

public class GetCompanyDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Type { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
    public bool SoftDeleted { get; set; }
}

public class PostCompanyDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Type { get; set; }
}