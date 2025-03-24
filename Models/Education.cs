using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{
    [Table("Education")]
    public class Education
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Type")]
        public string Type { get; set; }
        [Column("StartedAt")]
        public DateTime StartDate { get; set; }
        [Column("EndedAt")]
        public DateTime EndDate { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Degree")]
        public string Degree { get; set; }
        [Column("Place")]
        public string Place { get; set; }
        [Column("Active")]
        public bool Active { get; set; }
        [Column("createdAt")]
        public DateTime createdAt { get; set; }
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }
        [Column("deletedAt")]
        public DateTime deletedAt { get; set; }
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; }
        
        [ForeignKey("CV")]
        [Column("CVId")]
        public int PersonId { get; set; }
        public CV Cv { get; set; }
    }
}