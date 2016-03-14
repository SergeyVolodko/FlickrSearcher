using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using FlickrSearcher.Search;
using FlickrSearcher.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace FlickrSearcher.Tests
{
    public class SearchControllerTests
    {
        [Fact]
        public void routing()
        {
            // arrange
            var uri = @"http://localhost:62276/api/search?text=test&page=1";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var config = new HttpConfiguration();

            // act
            WebApiConfig.Register(config);
            var route = WebApi.RouteRequest(config, request);

            // asserts
            route.Controller.Should().Be<SearchController>();
            route.Action.Should().Be("Search");
        }

        [Theory]
        [ControllerAutoData]
        public void calls_search_service_search(
            [Frozen]ISearchService service,
            SearchController sut,
            string text,
            int page)
        {
            // act
            sut.Search(text, page);

            // assert
            service
                .Received()
                .Search(text, page);
        }

        [Theory]
        [ControllerAutoData]
        public void return_photos_found_by_service(
            [Frozen]ISearchService service,
            SearchController sut,
            string text,
            int page,
            List<Photo> photos)
        {
            // arrange
            service.Search(text, page)
                .Returns(photos);

            // act
            var actual = sut.Search(text, page);

            // assert
            actual
                .ShouldBeEquivalentTo(photos);
        }
    }
}
