using BrainsToDo.Enums;

namespace BrainsToDo.DTOModels;

public class PostResumeDTO
{
    public int Id {get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; }
    public string PictureURL { get; set; }
    public string Summary { get; set; }
    public Status Status { get; set; }
    
}

public class PostEducationDTO
{
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public string Degree { get; set; }
    public string Place { get; set; }
    public bool Active { get; set; }
    public Status Status { get; set; }
}

public class PostCertificationDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public DateTime ValidTo { get; set; }
    public Status Status { get; set; }
}

public class PostProjectDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Completed { get; set; }
    public Status Status { get; set; }
}

public class PostExperienceDTO
{
    public string Name { get; set; }
    public string Organisation { get; set; }
    public string Type { get; set; }
    public string Position { get; set; }
    public string Description { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public bool Active { get; set; }
    public Status Status { get; set; } 
}

public class PostSkillDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int Level { get; set; }
    public Status Status { get; set; }
}

public class PostReferenceDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Status Status { get; set; }
}



