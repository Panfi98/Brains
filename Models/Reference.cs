using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrainsToDo.Enums;

namespace BrainsToDo.Models;

[Table("Reference")] 
public class Reference
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [Column("FirstName")]
    public string FirstName { get; set; }
    [Column("LastName")]
    public string LastName { get; set; }
    [Column("Position")]
    public string Position { get; set; }
    [Column("Email")]
    public string Email { get; set; }
    [Column("PhoneNumber")]
    public string PhoneNumber { get; set; }
    [Column("Status")]
    public Status Status { get; set; }
    [Column("createdAt")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    [Column("updatedAt")] 
    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    [Column("deletedAt")] 
    public DateTime? deletedAt { get; set; } = null;
    [Column("SoftDeleted")]
    public bool SoftDeleted { get; set; } = false;
    
        
    [ForeignKey("Resume")]
    [Column("ResumeId")]
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
}