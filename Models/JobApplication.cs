using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models;

[Table("JobApplication")]
public class JobApplication
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [Column("Name")]
    public string Name { get; set; }
    [Column("Description")]
    public string Description { get; set; }
    [Column("Status")]
    public string Status { get; set; }
    [Column("ApliedAt")]
    public DateTime ApliedAt { get; set; }
    [Column("createdAt")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    [Column("updatedAt")] 
    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    [Column("deletedAt")] 
    public DateTime? deletedAt { get; set; } = null;
    [Column("SoftDeleted")]
    public bool SoftDeleted { get; set; } = false;  
    
    [ForeignKey("User")]
    [Column("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [ForeignKey("Resume")]
    [Column("ResumeId")]
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
}