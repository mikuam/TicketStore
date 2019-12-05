using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketStore.Services;

namespace TicketStore.Clients
{
    public class OmdbClient : IMovieRatingProvider
    {
        private const string ApiKey = "4a6d4e9b";
        private const string Username = "Mik";
        private const string Password = "****";

        private string OmdbApiUrl = "http://www.omdbapi.com/?apiKey=" + ApiKey + "&t=";

        private HttpClient _client;
        private readonly ILogger<OmdbClient> _logger;

        public OmdbClient(HttpClient client, ILogger<OmdbClient> logger)
        {
            _client = client;
            _logger = logger;

            var authToken = Encoding.ASCII.GetBytes($"{Username}:{Password}");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(authToken));
        }

        public async Task<IDictionary<string, decimal>> GetMovieRatings(IEnumerable<string> movieTitles)
        {
            try
            {
                var result = new Dictionary<string, decimal>();

                var ratings = await Task.WhenAll(movieTitles.Select(title => FetchSingleTitle(title)));
                foreach (var rating in ratings.Where(r => r != null))
                {
                    result.Add(rating.Value.Key, rating.Value.Value);
                }

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong when calling OMDB API");
                return new Dictionary<string, decimal>();
            }
        }

        public async Task<decimal> GetMovieRating(string movieTitle)
        {
            try
            {
                using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));

                var response = await _client.GetAsync(
                    OmdbApiUrl + movieTitle.Replace(" ", "+"),
                    cancellationTokenSource.Token);
                response.EnsureSuccessStatusCode();

                using var responseStream = await response.Content.ReadAsStreamAsync();
                var omdbRatings = await JsonSerializer.DeserializeAsync<MovieRatingDto>(responseStream);
                if (omdbRatings != null
                    && !string.IsNullOrWhiteSpace(omdbRatings.Title)
                    && omdbRatings.ImdbRating != default)
                {
                    return decimal.Parse(omdbRatings.ImdbRating);
                }

                return default;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong when calling OMDB API");
                return default;
            }
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
