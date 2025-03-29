using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models;

[Table("JobMatch")]
public class JobMatch
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [Column("MatchScore")]
    public int MatchScore { get; set; }
    
    [ForeignKey("Job")]
    [Column("JobId")]
    public int JobId { get; set; }
    public Job Job { get; set; }
}