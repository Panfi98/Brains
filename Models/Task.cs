using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{
    [Table("Tasks")]
    public class Task
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("descriptions")]
        public string Description { get; set; }
        [Column("createdAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        [Column("updatedAt")] 
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        [Column("deletedAt")] 
        public DateTime? deletedAt { get; set; } = null;
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; } = false;
        
        [ForeignKey("User")]
        [Column("userId")]
        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}