using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace BrainsToDo.Models;

[Table("Company")]
public class Company
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [Column("Name")]
    public string Name { get; set; }
    [Column("Description")]
    public string Description { get; set; }
    [Column("Address")]
    public string Address { get; set; }
    [Column("Type")]
    public string Type { get; set; }
    [Column("createdAt")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    [Column("updatedAt")] 
    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    [Column("deletedAt")]
    public DateTime? deletedAt { get; set; } = null; 
    [Column("SoftDeleted")]
    public bool SoftDeleted { get; set; }  = false;
}
