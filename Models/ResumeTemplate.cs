using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrainsToDo.Enums;

namespace BrainsToDo.Models
{
    [Table("ResumeTemplate")]
    public class ResumeTemplate
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Order")]
        public int Order { get; set; }
        [Column("CategoriesIncluded")]
        public int CategoriesIncluded { get; set; }
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
    }
}