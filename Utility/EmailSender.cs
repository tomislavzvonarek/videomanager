using Microsoft.AspNetCore.Identity.UI.Services;

namespace Utility;

public class EmailSender : IEmailSender
{
    //TODO: implement send email logic!
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Task.CompletedTask;
    }
}