using System.ComponentModel.DataAnnotations;
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
    public DateTime createdAt { get; set; }
    [Column("updatedAt")]
    public DateTime updatedAt { get; set; }
    [Column("deletedAt")]
    public DateTime deletedAt { get; set; }
    [Column("SoftDeleted")]
    public bool SoftDeleted { get; set; } 
}