using System.Text.Json.Serialization;

namespace BrainsToDo.DTOModels;

public class GetJobDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Place { get; set; }
    public string Position { get; set; }
  
    [JsonIgnore]
    public int? CompanyId { get; set; }
    
    public GetCompanyDTO Company { get; set; }
}