namespace BrainsToDo.DTOModels;
{
    public class PostUserDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class GetUserDTO
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}