using System.Text.Json.Serialization;

namespace BrainsToDo.DTOModels;

public class GetContactDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
    public bool SoftDeleted { get; set; }
    public GetJobDTO Job { get; set; }
    public GetCompanyDTO Company { get; set; } 
    
}

public class PostContactDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? JobId { get; set; }
    public int? CompanyId { get; set; }
   
}