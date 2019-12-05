using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TicketStore.Services;

namespace TicketStore.Clients
{
    public class OmdbClient : IMovieRatingProvider
    {
        private const string ApiKey = "4a6d4e9b";

        private string OmdbApiUrl = "http://www.omdbapi.com/?apiKey=" + ApiKey + "&t=";

        private HttpClient _client;

        public OmdbClient()
        {
            _client = new HttpClient();
        }

        public async Task<IDictionary<string, decimal>> GetMovieRatings(IEnumerable<string> movieTitles)
        {
            var result = new Dictionary<string, decimal>();

            var ratings = await Task.WhenAll(movieTitles.Select(title => FetchSingleTitle(title)));
            foreach (var rating in ratings.Where(r => r != null))
            {
                result.Add(rating.Value.Key, rating.Value.Value);
            }

            return result;
        }

        private async Task<KeyValuePair<string, decimal>?> FetchSingleTitle(string movieTitle)
        {
            var response = await _client.GetAsync(OmdbApiUrl + movieTitle.Replace(" ", "+"));
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var omdbRatings = await JsonSerializer.DeserializeAsync<MovieRatingDto>(responseStream);
            if (omdbRatings != null
                && !string.IsNullOrWhiteSpace(omdbRatings.Title)
                && omdbRatings.ImdbRating != default)
            {
                return new KeyValuePair<string, decimal>(movieTitle, decimal.Parse(omdbRatings.ImdbRating));
            }

            return null;
        }
    }
}
