using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models;

[Table("Mail")]
public class Mail
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [Column("Email")] 
    public string Email { get; set; }
    [Column("Code")]
    public string Code { get; set; } 
    [Column("ExpirationTime")]
    public DateTime ExpirationTime { get; set; }
    [Column("Attempts")] 
    public int Attempts { get; set; } = 3;
    [Column("createdAt")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; }= DateTime.UtcNow;
    [Column("updatedAt")]
    public DateTime updatedAt { get; set; }= DateTime.UtcNow;
    [Column("deletedAt")] 
    public DateTime? deletedAt { get; set; } = null;
    [Column("SoftDeleted")]
    public bool SoftDeleted { get; set; } = false;
    
    [ForeignKey("User")]
    [Column("UserId")]
    public int UserId { get; set; }

    public User User { get; set; }
}