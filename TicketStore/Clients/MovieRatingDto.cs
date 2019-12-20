using System.Text.Json.Serialization;

namespace TicketStore.Clients
{
    public class MovieRatingDto
    {
        public string Title { get; set; }

        [JsonPropertyName("imdbRating")]
        public string ImdbRating { get; set; }
    }
}
