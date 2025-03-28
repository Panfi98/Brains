using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{
    [Table("Tasks")]
    public class Tasks
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("names")]
        public string Name { get; set; }
        [Column("descriptions")]
        public string Description { get; set; }
        [Column("createdAt")]
        public DateTime createdAt { get; set; }
        [Column("updatedAt")]
        public DateTime updatedAT { get; set; }
        
        [ForeignKey("User")]
        [Column("userId")]
        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}