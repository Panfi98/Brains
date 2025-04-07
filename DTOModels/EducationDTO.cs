﻿using BrainsToDo.Models;

namespace BrainsToDo.DTOModels;

public class GetEducationDTO
{
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public string Degree { get; set; }
    public string Place { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool SoftDeleted { get; set; }
    public Person Person { get; set; }
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
    public int PersonId { get; set; }
}