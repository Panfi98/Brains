namespace BrainsToDo.Helpers;

public class Payload <T>
{
    public string RequestStatus {get; set;} = "Success";
    public T Data {get; set;}
    public string Message { get; set; }
}

public class PayloadList <T>
{
    public string RequestStatus {get; set;} = "Success";
    public T Data {get; set;}
    public string Message { get; set; }
}


