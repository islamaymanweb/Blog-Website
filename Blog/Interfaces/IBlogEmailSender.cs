using Microsoft.AspNetCore.Identity.UI.Services;

namespace Blog.Interfaces
{
    public interface IBlogEmailSender : IEmailSender
    {
        Task SendContactEmailAsync(string emailFrom, string name, string subject, string body);
    }
}
