using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Controllers;
using MoviesApp.Data;
using MoviesApp.Data.Models;
using MoviesApp.Data.Models.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Tests
{
    public class MoviesControllerTests
    {
        private static Builder _builder;
        private static DbContextOptions<MovieInMemoryDbContext> _options;
        private static Bogus.Faker _faker;

        [SetUp]
        public static void SetUp()
        {
            _faker = new Bogus.Faker();
            var builderSettings = new BuilderSettings();
            _builder = new Builder(builderSettings);
            _options = new DbContextOptionsBuilder<MovieInMemoryDbContext>().UseInMemoryDatabase(databaseName: "MoviesControllerTestDb").Options;
        }

        [Test]
        public static async Task GetMovies_Retunrs_MoviesApiResult()
        {
            //Arrange
            var sourceMoviesCount = 10;
            var movies = _builder.CreateListOfSize<Movie>(sourceMoviesCount).Build();

            using (var context = new MovieInMemoryDbContext(_options))
            {
                context.AddRange(movies);
                context.SaveChanges();
            }

            //Act
            MovieApiResult actual = null;
            using (var context = new MovieInMemoryDbContext(_options))
            {
                var movieRepository = new MovieRepository(context);
                var controller = new MoviesController(movieRepository);
                actual = (await controller.GetMovies()).Value;
                context.Database.EnsureDeleted();
            }

            //Assert
            actual.Should().NotBeNull();
            actual.Data.Count.Should().Be(sourceMoviesCount);
        }

        [Test]
        public static async Task GetMovies_With_SortColumn_And_DescSortOrder_Retunrs_DataInDescOrder()
        {
            //Arrange
            var sourceMoviesCount = 10;
            var sourcePageSize = 5;
            var sourceSortColumn = "language";
            var sourceOrder = "DESC";
            var movies = _builder.CreateListOfSize<Movie>(sourceMoviesCount)
                .TheFirst(3).With(x => x.Language = "Language1")
                .TheNext(3).With(x => x.Language = "Language3")
                .TheNext(3).With(x => x.Language = "Language2")
                .Build();

            using (var context = new MovieInMemoryDbContext(_options))
            {
                context.AddRange(movies);
                context.SaveChanges();
            }

            //Act
            MovieApiResult actual = null;
            using (var context = new MovieInMemoryDbContext(_options))
            {
                var movieRepository = new MovieRepository(context);
                var controller = new MoviesController(movieRepository);
                actual = (await controller.GetMovies(0, sourcePageSize, sourceSortColumn, sourceOrder)).Value;
                context.Database.EnsureDeleted();
            }

            //Assert
            actual.Should().NotBeNull();
            actual.Data.First().Language.Should().Be("Language3");
            
        }

        [Test]
        public static async Task GetMovies_With_TitleFilterQuery_Retunrs_DataFilteredByTitle()
        {
            //Arrange
            var sourceMoviesCount = 3;
            var sourceTitleQuery = "TheFi";
            var movies = _builder.CreateListOfSize<Movie>(sourceMoviesCount)
                .TheFirst(1).With(x => x.Title = "TheFirstTitle")
                .TheNext(1).With(x => x.Title = "theFirstMovieTitle")
                .TheLast(1).With(x => x.Title = "TheSecondTitle")
                .Build();
            var expectedMoviesCount = 2;

            using (var context = new MovieInMemoryDbContext(_options))
            {
                context.AddRange(movies);
                context.SaveChanges();
            }

            //Act
            MovieApiResult actual = null;
            using (var context = new MovieInMemoryDbContext(_options))
            {
                var movieRepository = new MovieRepository(context);
                var controller = new MoviesController(movieRepository);
                actual = (await controller.GetMovies(0, 10, null, null, sourceTitleQuery)).Value;
                context.Database.EnsureDeleted();
            }

            //Assert
            actual.Should().NotBeNull();
            actual.Data.Count(x => x.Title.ToLower().Contains(sourceTitleQuery.ToLower())).Should().Be(expectedMoviesCount);
        }
        [Test]
        public static async Task GetAllLanguages_Returns_DistinctLanguages()
        {
            //Arrange
            var sourceMoviesCount = 3;
            var expectedDistinctLanguages = 2;
            var movies = _builder.CreateListOfSize<Movie>(sourceMoviesCount)
                .TheFirst(1)
                .With(x => x.Language = "English")
                .TheNext(1)
                .With(x => x.Language = "english")
                .TheLast(1)
                .With(x => x.Language = "Danish")
                .Build();

            using (var context = new MovieInMemoryDbContext(_options))
            {
                context.AddRange(movies);
                context.SaveChanges();
            }

            //Act
            ICollection<string> actual = null;
            using (var context = new MovieInMemoryDbContext(_options))
            {
                var movieRepository = new MovieRepository(context);
                var controller = new MoviesController(movieRepository);
                actual = (await controller.GetAllLanguages());
                context.Database.EnsureDeleted();
            }

            //Assert
            actual.Should().NotBeNull();
            actual.Count.Should().Be(expectedDistinctLanguages);
        }

        [Test]
        public static async Task GetAllLocations_Returns_DistinctLocations()
        {
            //Arrange
            var sourceMoviesCount = 3;
            var expectedLocations = 3;
            var movies = _builder.CreateListOfSize<Movie>(sourceMoviesCount)
                .TheFirst(1)
                .With(x => x.Language = "Copenhagne")
                .TheNext(1)
                .With(x => x.Language = "London")
                .TheLast(1)
                .With(x => x.Language = "Delhi")
                .Build();

            using (var context = new MovieInMemoryDbContext(_options))
            {
                context.AddRange(movies);
                context.SaveChanges();
            }

            //Act
            ICollection<string> actual = null;
            using (var context = new MovieInMemoryDbContext(_options))
            {
                var movieRepository = new MovieRepository(context);
                var controller = new MoviesController(movieRepository);
                actual = (await controller.GetAllLocations());
                context.Database.EnsureDeleted();
            }

            //Assert
            actual.Should().NotBeNull();
            actual.Count.Should().Be(expectedLocations);
        }
    }
}
