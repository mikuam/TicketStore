using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TicketStore.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly ServiceConfiguration serviceConfiguration;
        private readonly SmtpClient _client;

        public EmailSenderService(IConfiguration configuration)
        {
            serviceConfiguration = new ServiceConfiguration();
            configuration.Bind(serviceConfiguration);

            _client = new SmtpClient(serviceConfiguration.Email.SmtpServerAddress, serviceConfiguration.Email.SmtpServerPort);
        }

        public async Task SendEmail(string emailAddress, string content)
        {
            var message = new MailMessage(serviceConfiguration.Email.SenderAddress, emailAddress)
            {
                Subject = content
            };

            await _client.SendMailAsync(message);
        }
    }
}
