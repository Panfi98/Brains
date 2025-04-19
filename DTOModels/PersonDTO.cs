namespace BrainsToDo.DTOModels;

public class GetPersonDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string PictureURL { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? deletedAt { get; set; }
    public bool SoftDeleted { get; set; } = false;
    public GetUserDTO User { get; set; }
}

public class PostPersonDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string PictureURL { get; set; }
    public DateTime BirthDate { get; set; }
    public int UserId { get; set; }
}

public class PunchPersonDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string PictureURL { get; set; }
    public DateTime BirthDate { get; set; }
}