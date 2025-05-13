using System.Text.Json.Serialization;

namespace BrainsToDo.DTOModels;

public class UserLogInDTO
{
    [JsonIgnore]
    int Id {get; set;}
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UserSignUpDTO
{
    [JsonIgnore]
    public int Id {get; set;}
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class VerifyEmailDTO
{
    public string Code { get; set; }
}

