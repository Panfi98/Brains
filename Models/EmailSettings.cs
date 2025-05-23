﻿namespace BrainsToDo.Models;

public class EmailSettings
{
    public string MailServer { get; set; } = string.Empty;
    public int MailPort { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool UseSsl { get; set; }
}