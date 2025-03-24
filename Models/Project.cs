using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{
    [Table("Project")]
    public class Project
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("StartedAt")]
        public DateTime StartDate { get; set; }
        [Column("CompletedAt")]
        public DateTime EndDate { get; set; }
        [Column("Completed")]
        public bool Completed { get; set; }
        [Column("createdAt")]
        public DateTime createdAt { get; set; }
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }
        [Column("deletedAt")]
        public DateTime deletedAt { get; set; }
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; }
        
        [ForeignKey("Experience")]
        [Column("ExperienceId")]
        public int ExperienceId { get; set; }
        public Experience Experience { get; set; }
        
        [ForeignKey("Education")]
        [Column("EducationId")]
        public int EducationId { get; set; }
        public Education Education { get; set; }
        
        [ForeignKey("CV")]
        [Column("CVId")]
        public int PersonId { get; set; }
        public CV Cv { get; set; }
    }
}