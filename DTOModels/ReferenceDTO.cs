﻿using BrainsToDo.Models;

namespace BrainsToDo.DTOModels;

public class PostReferenceDTOForresumeMaker
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Status Status { get; set; }
    public int ResumeId { get; set; }
    
}