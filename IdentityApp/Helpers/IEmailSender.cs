using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Helpers
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}