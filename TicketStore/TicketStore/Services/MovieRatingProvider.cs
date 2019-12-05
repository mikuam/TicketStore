using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketStore.Services
{
    public class MovieRatingProvider : IMovieRatingProvider
    {
        public async Task<IDictionary<string, decimal>> GetMovieRatings(IEnumerable<string> movieTitles)
        {
            var random = new Random();

            var ratings = movieTitles
                .Distinct()
                .Select(title => new KeyValuePair<string, decimal>(title, (decimal)random.Next(10, 50) / 10));

            return await Task.FromResult(new Dictionary<string, decimal> (ratings));
        }
    }
}
