namespace BrainsToDo.DTOModels;

    public class GetUserDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class UserSkillDTO
    {
        public string Name { get; set; }
        public List<SkillDTO> Skills { get; set; }
    }

    