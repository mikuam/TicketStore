using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketStore.Services
{
    public interface IMovieRatingProvider
    {
        Task<IDictionary<string, decimal>> GetMovieRatings(IEnumerable<string> movieTitles);

        Task<decimal> GetMovieRating(string movieTitle);
    }
}