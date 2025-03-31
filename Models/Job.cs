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
        public DateTime createdAt { get; set; }
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }
        [Column("deletedAt")]
        public DateTime deletedAt { get; set; }
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; }
        
        [ForeignKey("Company")]
        [Column("CompanyId")]
        public int? CompanyId { get; set; }
        public Company? Company  { get; set; }
        
        public List <Skill> Skills { get; set; } = [];
        public List <JobSkill> JobSkills { get; set; } = [];
        
    }
}