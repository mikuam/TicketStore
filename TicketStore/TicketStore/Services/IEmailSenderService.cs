using System.Threading.Tasks;

namespace TicketStore.Services
{
    public interface IEmailSenderService
    {
        Task SendEmail(string emailAddress, string content);
    }
}