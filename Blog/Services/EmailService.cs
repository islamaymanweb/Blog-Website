using Blog.Interfaces;
using Blog.ViewModels;
using Microsoft.Extensions.Options;

namespace Blog.Services
{
    public class EmailService : IBlogEmailSender
    {

        private readonly IOptions<MailSettings> _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings;
        }
        public Task SendContactEmailAsync(string emailFrom, string name, string subject, string body)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
