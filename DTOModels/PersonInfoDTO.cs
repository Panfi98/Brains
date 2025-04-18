using BrainsToDo.Models;
namespace BrainsToDo.DTOModels;

public class PersonInfoDTOForResumeMaker
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string PictureURL { get; set; }
    public DateTime BirthDate { get; set; }
    public Status Status { get; set; }
    public int ResumeId { get; set; }
}

