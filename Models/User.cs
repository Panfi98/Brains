using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models;

    [Table("User")]
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        
        
        [Column("firstName")]
        public string firstName { get; set; }
        
        
        [Column("lastName")]
        public string lastName { get; set; }
        
        
        [Column("createdAt")]
        public DateTime createdAt { get; set; }
        
        
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; } 
    }
