using System.Collections.Generic;

namespace TicketStore.Events
{
    public interface IMovieRatingProvider
    {
        IDictionary<string, decimal> GetMovieRatings(IEnumerable<string> movieTitles);
    }
}