using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models;

[Table("JobSkills")]
public class JobSkill
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    
    [ForeignKey("Job")]
    [Column("JobId")]
    public int JobId { get; set; }
    public Job Job { get; set; }
}