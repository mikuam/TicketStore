using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketStore.Events
{
    public class EventProvider
    {
        public IEnumerable<Event> GetActiveEvents()
        {
            var events =  GetAllEvents();

            return ApplyRatings(events);
        }

        private IEnumerable<Event> ApplyRatings(IEnumerable<Event> events)
        {
            var ratingsProvider = new MovieRatingProvider();
            var movieRatings = ratingsProvider.GetMovieRatings(
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
