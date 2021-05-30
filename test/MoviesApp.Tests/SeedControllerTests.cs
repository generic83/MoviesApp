using FizzWare.NBuilder;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using MoviesApp.Data.Models;
using MoviesApp.Controllers;
using MoviesApp.Converters;
using MoviesApp.SystemIo;

namespace MoviesStore.Tests.Controllers
{
    [TestFixture]
    public class SeedControllerTests
    {
        private static Builder _builder;
        private static IJsonConvert _jsonConvert;
        private static IFile _file;

        [SetUp]
        public static void SetUp()
        {
            var builderSettings = new BuilderSettings();
            _builder = new Builder(builderSettings);
            _jsonConvert = Substitute.For<IJsonConvert>();
            _file = Substitute.For<IFile>();
        }

        [Test]
        public static async Task Import_Seeds_MoviesJsonData()
        {
            var sourceFilePath = "Data/Source/movies.json";
            var sourceStream = new MemoryStream();
            _file.OpenRead(Arg.Any<string>()).Returns(sourceStream);
            var deserializedJsonSource = _builder.CreateListOfSize<MovieJsonModel>(10).Build();
            
            _jsonConvert.DeserializeAsync<ICollection<MovieJsonModel>>(sourceStream, Arg.Is<JsonSerializerOptions>(x => x.PropertyNameCaseInsensitive == true))
                .Returns(deserializedJsonSource);

            var controller = new SeedController(_jsonConvert, _file);
            var result = await controller.Import() as JsonResult;

            _file.Received(1).OpenRead(sourceFilePath);
            await _jsonConvert.Received(1).DeserializeAsync<ICollection<MovieJsonModel>>(sourceStream, Arg.Any<JsonSerializerOptions>());

            result.Value.Should().NotBeNull();
        }
    }
}
