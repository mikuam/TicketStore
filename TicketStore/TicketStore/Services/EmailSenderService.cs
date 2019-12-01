using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TicketStore.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;

        private readonly SmtpClient _client;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;

            var smtpServerAddress = _configuration.GetValue<string>("Email:smtpServerAddress");
            var smtpServerPort = _configuration.GetValue<int>("Email:smtpServerPort");
            _client = new SmtpClient(smtpServerAddress, smtpServerPort);
        }

        public async Task SendEmail(string emailAddress, string content)
        {
            var fromAddress = _configuration.GetValue<string>("Email:senderAddress");
            var message = new MailMessage(fromAddress, emailAddress);
            message.Subject = content;

            await _client.SendMailAsync(message);
        }
    }
}
