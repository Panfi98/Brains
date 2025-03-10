using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{
    [Table("Tasks")]
    public class Tasks
    {
        [Key]
        [Column("taskId")]
        public int id { get; set; }
        [Column("names")]
        public string name { get; set; }
        [Column("descriptions")]
        public string description { get; set; }
        [Column("createdAt")]
        public DateTime createdAt { get; set; }
        [Column("updatedAt")]
        public DateTime updatedAT { get; set; }
        [Column("userId")]
        public int userId { get; set; }
    }
}