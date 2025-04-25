using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrainsToDo.Enums;

namespace BrainsToDo.Models
{
    [Table("InfoSkill")]
    public class InfoSkill
    {
        [Key] [Column("Id")] 
        public int Id { get; set; }
        [Column("Name")] 
        public string Name { get; set; }
        [Column("Description")] 
        public string Description { get; set; }
        [Column("Type")] 
        public string Type { get; set; }
        [Column("Level")] 
        public int Level { get; set; }
        [Column("Status")] 
        public Status Status { get; set; }

        [Column("createdAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        [Column("updatedAt")] 
        public DateTime updatedAt { get; set; }
        [Column("deletedAt")] 
        public DateTime? deletedAt { get; set; } = null;
        [Column("SoftDeleted")] 
        public bool SoftDeleted { get; set; } = false;

        [ForeignKey("Resume")]
        [Column("ResumeId")]
        public int? ResumeId { get; set; }

        public Resume? Resume { get; set; }
    }
}