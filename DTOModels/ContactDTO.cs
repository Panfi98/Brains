using System.Text.Json.Serialization;

namespace BrainsToDo.DTOModels;

public class GetContactDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    [JsonIgnore]
    public int? CompanyId { get; set; }
    
    public GetCompanyDTO Company { get; set; }
    
    [JsonIgnore]
    public int? JobId { get; set; }
    
    public GetJobDTO Job { get; set; }
}