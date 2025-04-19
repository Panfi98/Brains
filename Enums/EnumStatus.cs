using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BrainsToDo.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Started,
    InProgress,
    Finished
}