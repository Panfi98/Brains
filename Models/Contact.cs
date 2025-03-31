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
    public DateTime createdAt { get; set; }
    [Column("updatedAt")]
    public DateTime updatedAt { get; set; }
    [Column("deletedAt")]
    public DateTime deletedAt { get; set; }
    [Column("SoftDeleted")]
    public bool SoftDeleted { get; set; }
    
    [ForeignKey("Company")]
    [Column("CompanyId")]
    public int? CompanyID { get; set; }
    public Company? Company { get; set; }
    
    [ForeignKey("Job")]
    [Column("JobId")]
    public int? JobId { get; set; }
    public Job? Job { get; set; }
}