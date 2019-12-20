using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketStore.Data;
using TicketStore.Services;

namespace TicketStore.Events
{
    public class EventProvider : IEventProvider
    {
        private readonly ILocalDBContext _localDbContext;
        private readonly IMovieRatingProvider _movieRatingProvider;

        public EventProvider(ILocalDBContext localDbContext, IMovieRatingProvider movieRatingProvider)
        {
            _localDbContext = localDbContext;
            _movieRatingProvider = movieRatingProvider;
        }

        public async Task<IEnumerable<EventWithRating>> GetActiveEvents()
        {
            var events = await _localDbContext.Events.ToListAsync();

            return await ApplyRatings(events);
        }

        private async Task<IEnumerable<EventWithRating>> ApplyRatings(IEnumerable<Event> events)
        {
            var eventsToReturn = events.Select(e => new EventWithRating(e));

            var movieRatings = await _movieRatingProvider.GetMovieRatings(
                eventsToReturn.Where(e => e.Type == EventType.Movie)
                    .Select(m => m.Title));

            foreach (var rating in movieRatings)
            {
                var eventToRate = eventsToReturn.FirstOrDefault(e => e.Title == rating.Key);
                if (eventToRate != null)
                {
                    eventToRate.Rating = rating.Value;
                }
            }

            return eventsToReturn;
        }
    }
}
