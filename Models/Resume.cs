using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrainsToDo.Enums;

namespace BrainsToDo.Models
{
    [Table("Resume")]
    public class Resume
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Column("LastName")]
        public string LastName { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Column("Address")]
        public string Address { get; set; }
        [Column("PictureURL")]
        public string PictureURL { get; set; }
        [Column("BirthDate")]
        public DateTime BirthDate { get; set; }
        [Column("Summary")]
        public string Summary { get; set; }
        [Column("Status")]
        public Status Status { get; set; }
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
        [Column("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [ForeignKey("ResumeTemplate")]
        [Column("ResumeTemplateId")]
        public int? ResumeTemplateId { get; set; }
        public ResumeTemplate? ResumeTemplate { get; set; }
        
        
        
        
        
    }
}