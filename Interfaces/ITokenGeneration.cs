using BrainsToDo.Models;

namespace BrainsToDo.Interfaces;

public interface ITokenGeneration
{ 
    string GenerateToken(User user);
}
