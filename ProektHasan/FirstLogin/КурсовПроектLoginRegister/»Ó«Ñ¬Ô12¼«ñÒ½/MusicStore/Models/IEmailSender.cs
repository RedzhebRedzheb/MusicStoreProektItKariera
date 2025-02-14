using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Implement your email sending logic here.
        // For demonstration purposes, we will just log it to the console.

        Console.WriteLine($"Sending email to {email} with subject {subject}");
        Console.WriteLine($"Message: {htmlMessage}");

        // Simulate sending an email asynchronously
        return Task.CompletedTask;
    }
}
