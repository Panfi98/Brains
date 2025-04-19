
namespace BrainsToDo.DTOModels;

public class PostResumeForResumeMaker
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; }
    
    public string PictureURL { get; set; }
    public string Summary { get; set; }
    public string Status { get; set; }
    
}

public class PostEducationForResumeMaker
{
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public string Degree { get; set; }
    public string Place { get; set; }
    public bool Active { get; set; }
    public string Status { get; set; }
}

public class PostCertificationForResumeMaker
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public DateTime ValidTo { get; set; }
    public string Status { get; set; }
}

public class PostProjectForResumeMaker
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Completed { get; set; }
    public string Status { get; set; }
}

public class PostExperienceForResumeMaker
{
    public string Name { get; set; }
    public string Organisation { get; set; }
    public string Type { get; set; }
    public string Position { get; set; }
    public string Description { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public bool Active { get; set; }
    public string Status { get; set; } 
}

public class PostSkillForResumeMaker
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int Level { get; set; }
    public string Status { get; set; }
}

public class ResumeTemplateForResumeMaker
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Status { get; set; }
}



