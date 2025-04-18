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
        public string Status { get; set; }
        [Column("createdAt")]
        public DateTime createdAt { get; set; }
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }
        [Column("deletedAt")] 
        public DateTime? deletedAt { get; set; } = null;
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; } = false;
        
        [ForeignKey("Person")]
        [Column("PersonId")]
        public int? PersonId { get; set; }
        public Person? Person { get; set; }
        
        [ForeignKey("ResumeTemplate")]
        [Column("ResumeTemplateId")]
        public int? ResumeTemplateId { get; set; }
        public ResumeTemplate? ResumeTemplate { get; set; }
        
        
        
        
    }
}