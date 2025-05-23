using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrainsToDo.Enums;

namespace BrainsToDo.Models
{
    [Table("Project")]
    public class Project
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("StartedAt")]
        public DateTime StartDate { get; set; }
        [Column("CompletedAt")]
        public DateTime EndDate { get; set; }
        [Column("Completed")]
        public bool Completed { get; set; }
        [Column("Status")]
        public Status Status { get; set; }
        [Column("createdAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime createdAt { get; set; }= DateTime.UtcNow;
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }  = DateTime.UtcNow;
        [Column("deletedAt")] 
        public DateTime? deletedAt { get; set; } = null;
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; } = false;
        
        
       /* [ForeignKey("Experience")]
        [Column("ExperienceId")]
        public int ExperienceId { get; set; }
        public Experience Experience { get; set; }
        
        [ForeignKey("Education")]
        [Column("EducationId")]
        public int EducationId { get; set; }
        public Education Education { get; set; }*/
        
        [ForeignKey("Resume")]
        [Column("ResumeId")]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}