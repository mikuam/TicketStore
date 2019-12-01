using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketStore.Services
{
    public class MovieRatingProvider : IMovieRatingProvider
    {
        public IDictionary<string, decimal> GetMovieRatings(IEnumerable<string> movieTitles)
        {
            var random = new Random();

            var ratings = movieTitles
                .Distinct()
                .Select(title => new KeyValuePair<string, decimal>(title, (decimal)random.Next(10, 50) / 10));

            return new Dictionary<string, decimal> (ratings);
        }
    }
}
