using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrainsToDo.Enums;

namespace BrainsToDo.Models
{
    [Table("Experience")]
    public class Experience
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Organisation")]
        public string Organisation { get; set; }
        [Column("Type")]
        public string Type { get; set; }
        [Column("Position")]
        public string Position { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("StartedAt")]
        public DateTime StartedAt { get; set; }
        [Column("EndedAt")]
        public DateTime EndedAt { get; set; }
        [Column("Active")]
        public bool Active { get; set; }
        [Column("createdAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime createdAt { get; set; }= DateTime.UtcNow;
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }  = DateTime.UtcNow;
        [Column("deletedAt")] 
        public DateTime? deletedAt { get; set; } = null;
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; } = false;
        [Column("Status")]
        public Status Status { get; set; }
        
        [ForeignKey("Resume")]
        [Column("ResumeId")]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}