using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models;

[Table("UserSkill")]
public class UserSkill
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    
    [ForeignKey("User")]
    [Column("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
}