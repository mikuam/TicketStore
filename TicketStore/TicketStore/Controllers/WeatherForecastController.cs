using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TicketStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // GET: weatherForecast/
        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromQuery]bool sortByTemperature = false)
        {
            var rng = new Random();
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });

            if (sortByTemperature)
            {
                forecasts = forecasts.OrderByDescending(f => f.TemperatureC);
            }

            return forecasts;
        }

        // GET: weatherForecast/3
        [Route("{daysForward}")]
        [HttpGet]
        public WeatherForecast Get(int daysForward)
        {
            var rng = new Random();
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(daysForward),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };
        }

        // POST: weatherForecast/
        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast forecast, [FromHeader] string parentRequestId)
        {
            Console.WriteLine($"Got a forecast for data: {forecast.Date} with parentRequestId: {parentRequestId}!");
            return new AcceptedResult();
        }

        // POST: weatherForecast/sendfile
        [Route("sendfile")]
        [HttpPost]
        public IActionResult SaveFile([FromForm] string fileName, [FromForm] IFormFile file)
        {
            Console.WriteLine($"Got a file with name: {fileName} and size: {file.Length}");
            return new AcceptedResult();
        }
    }
}
