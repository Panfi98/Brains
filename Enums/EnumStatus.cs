using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BrainsToDo.Enums;

public enum Status
{
    NotStarted,
    InProgress,
    Finished
}