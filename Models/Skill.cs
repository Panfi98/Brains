using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{   
    [Table("Skills")]
    public class Skill
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Type")]
        public string Type { get; set; }
        [Column("Level")]
        public int Level { get; set; }
        [Column("createdAt")]
        public DateTime createdAt { get; set; }
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }
        [Column("deletedAt")]
        public DateTime deletedAt { get; set; }
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; }
        
        [ForeignKey("Person")]
        [Column("PersonId")]
        public int? PersonId { get; set; }
        public Person? Person { get; set; }
        
        [ForeignKey("Project")]
        [Column("ProjectId")]
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
        
        [ForeignKey("Experience")]
        [Column("ExperienceId")]
        public int? ExperienceId { get; set; }
        public Experience? Experience { get; set; }
        
        [ForeignKey("Education")]
        [Column("EducationId")]
        public int? EducationId { get; set; }
        public Education? Education { get; set; }

        public List<User> Users { get; } = [];
        public List<UserSkill> UserSkills { get; } = [];
        
        public List<Job> Jobs { get; } = [];
        public List<JobSkill> JobSkills { get; } = [];
    }
}