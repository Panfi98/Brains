using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Models
{   
    [Table("PersonInfo")]
    public class PersonInfo
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
        [Column("BirthDate")]
        public DateTime BirthDate { get; set; }
        [Column("PictureURL")]
        public string PictureURL { get; set; }
        [Column("Status")]
        public Status Status { get; set; }
        [Column("createdAt")]
        public DateTime createdAt { get; set; }
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }
        [Column("deletedAt")] 
        public DateTime? deletedAt { get; set; } = null;
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; } = false;
        
        [ForeignKey("Resume")]
        [Column("ResumeId")]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
    
    
}