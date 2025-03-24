using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models
{
    [Table("User_UserTypes")]
    public class User_UserType
    {   
        [Key]
        [ForeignKey("UserId")]
        [Column("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [Key]
        [ForeignKey("UserTypeId")]
        [Column("UserTypeId")]
        public int UserTypeId { get; set; }
        public User UserType { get; set; }
    }

}