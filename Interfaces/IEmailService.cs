﻿namespace BrainsToDo.Interfaces;

public interface IEmailService
{
    Task SendVerificationEmailAsync(string email, string verificationCode);
}