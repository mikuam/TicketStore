using System.Threading.Tasks;

namespace TicketStore.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task SendEmail(string emailAddress, string content)
        {
            await Task.CompletedTask;
        }
    }
}
