using System.Collections.Generic;

namespace TicketStore.Services
{
    public interface IMovieRatingProvider
    {
        IDictionary<string, decimal> GetMovieRatings(IEnumerable<string> movieTitles);
    }
}