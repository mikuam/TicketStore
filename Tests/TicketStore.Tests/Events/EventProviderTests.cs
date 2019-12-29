using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketStore.Data;
using TicketStore.Events;
using TicketStore.Services;

namespace TicketStore.Tests.Events
{
    [TestFixture]
    public class Tests
    {
        private IEventProvider eventProvider { get; set; }
        private IMovieRatingProvider movieRatingProvider;
        private aspnetcoreContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<aspnetcoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            context = new aspnetcoreContext(options);
            movieRatingProvider = Substitute.For<IMovieRatingProvider>();

            eventProvider = new EventProvider(context, movieRatingProvider);
        }

        [Test]
        public async Task GetActiveEvents_GivenEmptyEventList_ReturnsEmptyList()
        {
            // Act
            var activeEvents = await eventProvider.GetActiveEvents();

            // Assert
            Assert.AreEqual(0, activeEvents.Count());
            await movieRatingProvider.DidNotReceive().GetMovieRatings(Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public async Task GetActiveEvents_GivenEventButNoRating_ReturnsEventWithoutRating()
        {
            // Arrange
            var existingEvent = new Event() { Id = 1, Type = EventType.Movie, Title = "Shrek 18" };
            context.Add(existingEvent);
            context.SaveChanges();
            movieRatingProvider.GetMovieRatings(Arg.Any<IEnumerable<string>>()).Returns(new Dictionary<string, decimal>());

            // Act
            var result = await eventProvider.GetActiveEvents();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(existingEvent.Id, result.First().Id);
            Assert.AreEqual(default(decimal), result.First().Rating);
        }

        [Test]
        public async Task GetActiveEvents_GivenEventButNotAMovie_ReturnsEventWithoutRating()
        {
            // Arrange
            var existingEvent = new Event() { Id = 2, Type = EventType.Concert, Title = "Ed Sheeran Live" };
            context.Add(existingEvent);
            context.SaveChanges();

            // Act
            var result = await eventProvider.GetActiveEvents();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(existingEvent.Id, result.First().Id);
            Assert.AreEqual(default(decimal), result.First().Rating);
        }

        [Test]
        public async Task GetActiveEvents_GivenEventAndRating_ReturnsEventWithRating()
        {
            // Arrange
            var existingEvent = new Event() { Id = 1, Type = EventType.Movie, Title = "Shrek 18" };
            context.Add(existingEvent);
            context.SaveChanges();
            var rating = 7.9m;
            movieRatingProvider
                .GetMovieRatings(Arg.Any<IEnumerable<string>>())
                .Returns(new Dictionary<string, decimal> { { existingEvent.Title, rating } });

            // Act
            var result = await eventProvider.GetActiveEvents();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(rating, result.First().Rating);
        }
    }
}