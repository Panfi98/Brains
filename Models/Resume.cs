using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{
    [Table("Resume")]
    public class Resume
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
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
        public int PersonId { get; set; }
        public Person Person { get; set; }
        
        [ForeignKey("User")]
        [Column("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [ForeignKey("ResumeTemplate")]
        [Column("ResumeTemplateId")]
        public int ResumeTemplateId { get; set; }
        public ResumeTemplate ResumeTemplate { get; set; }
        
        
    }
}