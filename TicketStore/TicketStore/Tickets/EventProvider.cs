using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketStore.Services;

namespace TicketStore.Tickets
{
    public class EventProvider : IEventProvider
    {
        private readonly IMovieRatingProvider _movieRatingProvider;

        public EventProvider(IMovieRatingProvider movieRatingProvider)
        {
            _movieRatingProvider = movieRatingProvider;
        }

        public async Task<IEnumerable<Event>> GetActiveEvents()
        {
            var events =  GetAllEvents();

            return await ApplyRatings(events);
        }

        private async Task<IEnumerable<Event>> ApplyRatings(IEnumerable<Event> events)
        {
            var movieRatings = await _movieRatingProvider.GetMovieRatings(
                events.Where(e => e.Type == EventType.Movie)
                      .Select(m => m.Title));

            foreach (var rating in movieRatings)
            {
                var eventToRate = events.FirstOrDefault(e => e.Title == rating.Key);
                if (eventToRate != null)
                {
                    eventToRate.Rating = rating.Value;
                }
            }

            return events;
        }

        private static IEnumerable<Event> GetAllEvents()
        {
            return new List<Event>
            {
                new Event
                {
                    EventId = 1,
                    Title = "Ed Sheeran Live",
                    Type = EventType.Concert,
                    Date = DateTime.Today.AddDays(3)
                },
                new Event
                {
                    EventId = 2,
                    Title = "Madonna World Tour",
                    Type = EventType.Concert,
                    Date = DateTime.Today.AddDays(10)
                },
                new Event
                {
                    EventId = 3,
                    Title = "Pulp fiction",
                    Type = EventType.Movie,
                    Date = DateTime.Today
                },
                new Event
                {
                    EventId = 4,
                    Title = "Forrest Gump",
                    Type = EventType.Movie,
                    Date = DateTime.Today.AddDays(1)
                }
            };
        }
    }
}
