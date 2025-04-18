namespace BrainsToDo.DTOModels;

public class GetResumeTemplateDTO
{
    public string Name { get; set; }
    public string Order { get; set; }
    public string CategoriesIncluded { get; set; }
    
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
    public bool SoftDeleted { get; set; }
}

public class PostResumeTemplateDTO
{
    public string Name { get; set; }
    public string Order { get; set; }
    public string CategoriesIncluded { get; set; }
}