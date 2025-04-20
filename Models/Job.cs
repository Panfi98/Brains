using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{
    [Table("Job")]
    public class Job
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Place")]
        public string Place { get; set; }
        [Column("Position")]
        public string Position { get; set; }
        [Column("createdAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        [Column("updatedAt")] 
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        [Column("deletedAt")] 
        public DateTime? deletedAt { get; set; } = null;
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; } = false;
        
        [ForeignKey("Company")]
        [Column("CompanyId")]
        public int? CompanyId { get; set; }
        public Company Company  { get; set; }
        
        public List <Skill> Skills { get;  } = [];
        public List <JobSkill> JobSkills { get; } = [];
        
        public List <User> Users { get; } = [];
        public List <JobUser> JobUsers { get; } = [];
        
    }
}