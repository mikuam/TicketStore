using System.Net.Mail;
using System.Threading.Tasks;

namespace TicketStore.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailConfiguration emailConfiguration;
        private readonly SmtpClient _client;

        public EmailSenderService(ServiceConfiguration configuration)
        {
            emailConfiguration = configuration.Email;
            _client = new SmtpClient(emailConfiguration.SmtpServerAddress, emailConfiguration.SmtpServerPort);
        }

        public async Task SendEmail(string emailAddress, string content)
        {
            var message = new MailMessage(emailConfiguration.SenderAddress, emailAddress)
            {
                Subject = content
            };

            await _client.SendMailAsync(message);
        }
    }
}
