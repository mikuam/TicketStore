using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TicketStore.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public EventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("LocalDB"));
        }
        
        public async Task<IEnumerable<Event>> GetAll()
        {
            return await GetOpenedConnection().QueryAsync<Event>(SQL.GetAll);
        }

        public async Task<Event> GetWithTickets(int eventId)
        {
            var eventWithTickets = await GetOpenedConnection().QueryAsync<EventWithTicketFlat>(SQL.GetWithTickets, new { id = eventId });
            if (eventWithTickets != null && eventWithTickets.Any())
            {
                var firstEvent = eventWithTickets.First();
                var myEvent = new Event
                {
                    Id = firstEvent.Id,
                    Title = firstEvent.Title,
                    Type = firstEvent.Type,
                    Date = firstEvent.Date,
                    Rows = firstEvent.Rows,
                    Seats = firstEvent.Seats,
                    Tickets = new List<Ticket>()
                };

                if (firstEvent.TicketId != default)
                {
                    foreach (var eventWithTicketFlat in eventWithTickets)
                    {
                        myEvent.Tickets.Add(new Ticket
                        {
                            Id = eventWithTicketFlat.TicketId,
                            EventId = eventWithTicketFlat.Id,
                            Email = eventWithTicketFlat.Email,
                            Phone = eventWithTicketFlat.Phone,
                            Row = eventWithTicketFlat.Row,
                            Seat = eventWithTicketFlat.Seat
                        });
                    }
                }

                return myEvent;
            }

            return null;
        }

        public async Task AddEvent(Event newEvent)
        {
            await GetOpenedConnection().ExecuteAsync(SQL.AddEvent, newEvent);
        }

        public async Task RemoveEvent(int eventId)
        {
            await GetOpenedConnection().ExecuteAsync(SQL.RemoveEvent, new { id = eventId });
        }

        public async Task AddTicket(Ticket ticket)
        {
            await GetOpenedConnection().ExecuteAsync(SQL.AddTicket, ticket);
        }

        private IDbConnection GetOpenedConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }

            return _connection;
        }
    }

    internal static class SQL
    {
        internal const string GetAll = @"
            SELECT *
            FROM Events";

        internal const string GetWithTickets = @"
            SELECT e.*, t.Id as TicketId, t.[Row], t.Seat, t.Email, t.Phone
            FROM [aspnetcore].[dbo].[Events] e
	        LEFT JOIN [aspnetcore].[dbo].[Tickets] t ON e.Id = t.EventId
	        WHERE e.Id = @id";

        internal const string AddEvent = @"
            INSERT INTO [aspnetcore].[dbo].[Events] ([Title], [Date], [Type], [Rows], [Seats])
            VALUES(@Title, @Date, @Type, @Rows, @Seats)";

        internal const string RemoveEvent = @"
            DELETE FROM [aspnetcore].[dbo].[Tickets] WHERE EventId = @Id
            DELETE FROM [aspnetcore].[dbo].[Events] WHERE Id = @Id";

        internal const string AddTicket = @"
            INSERT INTO [aspnetcore].[dbo].[Tickets] ([EventId], [Row], [Seat], [Email], [Phone])
            VALUES (@EventId, @Row, @Seat, @Email, @Phone)";
    }
}
