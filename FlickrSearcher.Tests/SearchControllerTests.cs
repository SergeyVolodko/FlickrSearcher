using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using FlickrSearcher.Search;
using FlickrSearcher.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace FlickrSearcher.Tests
{
    public class SearchControllerTests
    {
        [Fact]
        public void routing()
        {
            // setup
            var uri = @"http://localhost:62276/api/search";
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var config = new HttpConfiguration();

            // act
            WebApiConfig.Register(config);
            var route = WebApi.RouteRequest(config, request);

            // asserts
            route.Controller.Should().Be<SearchController>();
            route.Action.Should().Be("Search");
        }
    }
}
