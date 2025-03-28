using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{   
    [Table("Sertification")]
    public class Сertification
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Date")]
        public DateTime Date { get; set; }
        [Column("URL")]
        public string Url { get; set; }
        [Column("Type")]
        public string Type { get; set; }
        [Column("ValidTo")]
        public DateTime ValidTo { get; set; }
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
        public int CvId { get; set; }
        public CV Cv { get; set; }
    }
}