using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainsToDo.Models;

    [Table("User")]
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("EmailConfirmed")] 
        public bool EmailConfirmed { get; set; } = false;
        [Column("createdAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime createdAt { get; set; }= DateTime.UtcNow;
        [Column("updatedAt")]
        public DateTime updatedAt { get; set; }= DateTime.UtcNow;
        [Column("deletedAt")] 
        public DateTime? deletedAt { get; set; } = null;
        [Column("SoftDeleted")]
        public bool SoftDeleted { get; set; } = false;

        public List<Skill> Skills { get; } = [];
        public List<UserSkill> UserSkills { get; } = [];

        public List<Job> Jobs { get; } = [];
        public List<JobUser> JobUsers { get; } = [];
    }
