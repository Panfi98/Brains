using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{   
    [Table("Person")]
    public class Person
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
        [Column("createdAt")]
        public DateTime createdAt { get; set; }
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }
        [Column("deletedAt")]
        public DateTime deletedAt { get; set; }
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; }
        
        [ForeignKey("User")]
        [Column("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}