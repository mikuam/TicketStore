namespace TicketStore.Services
{
    public class EmailConfiguration
    {
        public string SmtpServerAddress { get; set; }

        public int SmtpServerPort { get; set; }

        public string SenderAddress { get; set; }
    }
}
