using BrainsToDo.Models;

namespace BrainsToDo.DTOModels;

public class GetResumeDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string PictureURL { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
    public bool SoftDeleted { get; set; }
    
    public GetPersonDTO Person { get; set; }
    public GetResumeTemplateDTO ResumeTemplate { get; set; }
}

public class PostResumeDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string PictureURL { get; set; }
    
    public int? PersonId { get; set; }
    public int? ResumeTemplateId { get; set; }
}
