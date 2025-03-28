using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
    [Column("createdAt")]
    public DateTime createdAt { get; set; }
    [Column("updatedAt")]
    public DateTime updatedAt { get; set; }
    [Column("deletedAt")]
    public DateTime deletedAt { get; set; }
    [Column("SoftDeleted")]
    public bool SoftDeleted { get; set; }
        
    [ForeignKey("Resume")]
    [Column("ResumeId")]
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
}