﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models;
[Table("CoverLetter")]
public class CoverLetter
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [Column("Description")]
    public string Description { get; set; }
    [Column("createdAt")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    [Column("updatedAt")] 
    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    [Column("deletedAt")] 
    public DateTime? deletedAt { get; set; } = null;
    [Column("SoftDeleted")]
    public bool SoftDeleted { get; set; } = false;
    
    [ForeignKey("JobApplicational")]
    [Column("JobApplicationalId")]
    public int JobApplicationId { get; set; }
    public JobApplication JobApplication { get; set; }
}