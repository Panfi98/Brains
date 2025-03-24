using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{
    [Table("WorkUserMatches")]
    public class WorkUserMatch
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
    }
}