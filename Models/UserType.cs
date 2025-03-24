using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{
    [Table("UserType")]
    public class UserType
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("MatchCriteria")]
        public string MatchCriteria { get; set; }
        [Column("createdAt")]
        public DateTime createdAt { get; set; }
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }
        [Column("deletedAt")]
        public DateTime deletedAt { get; set; }
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; }
        
        [ForeignKey("UserId")]
        [Column("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}