using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models;

[Table("Contact")]
public class Contact
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [Column("Name")]
    public string Name { get; set; }
    [Column("Email")]
    public string Email { get; set; }
    [Column("PhoneNumber")]
    public string PhoneNumber { get; set; }
    [Column("createdAt")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    [Column("updatedAt")] 
    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    [Column("deletedAt")] 
    public DateTime? deletedAt { get; set; } = null;
    [Column("SoftDeleted")]
    public bool SoftDeleted { get; set; } = false;
    
    [ForeignKey("Company")]
    [Column("CompanyId")]
    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
    
    [ForeignKey("Job")]
    [Column("JobId")]
    public int? JobId { get; set; }
    public Job? Job { get; set; }
    
}