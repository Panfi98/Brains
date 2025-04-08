namespace BrainsToDo.DTOModels;

public class GetJobDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Place { get; set; }
    public string Position { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
    public bool SoftDeleted { get; set; }
    public GetCompanyDTO Company { get; set; } 
}

public class PostJobDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Place { get; set; }
    public string Position { get; set; }
    public int? CompanyId { get; set; }
}

public class JobSkillDTO
    {
        public string Name { get; set; }
        public List<SkillDTO> Skills { get; set; }
    }
    