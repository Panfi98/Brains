namespace BrainsToDo.DTOModels;

    public class GetUserDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool SoftDeleted { get; set; }
    }

    public class PostUserDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }


    