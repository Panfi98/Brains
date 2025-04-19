using BrainsToDo.Models;

namespace BrainsToDo.DTOModels;

public class PostReferenceDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Status { get; set; }
    public int ResumeId { get; set; }
    
}