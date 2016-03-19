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
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace FlickrSearcher.Tests.PhotoControllerTests
{
    public class ProxyImage
    {
        [Fact]
        public void routing()
        {
            // arrange
            var uri = @"http://localhost:62276/api/image/1/42/id_sec/large";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var config = new HttpConfiguration();

            // act
            WebApiConfig.Register(config);
            var route = WebApi.RouteRequest(config, request);

            // asserts
            route.Controller.Should().Be<PhotoController>();
            route.Action.Should().Be("ProxyImage");
        }

        [Theory]
        [ControllerAutoData]
        public void calls_imege_proxy_redirect(
            [Frozen]IImageProxy proxy,
            PhotoController sut,
            int farm,
            int server,
            string id_secret,
            string size)
        {
            // act
            sut.ProxyImage(farm, server, id_secret, size);

            // assert
            proxy.Received()
                .Redirect(farm, server, id_secret, size);
        }


        //[Theory]
        //[ControllerAutoData]
        //public async Task returns_proxy_redirect_response(
        //    [Frozen]IImageProxy proxy,
        //    PhotoController sut,
        //    int farm,
        //    int server,
        //    string id_secret,
        //    string size,
        //    Task<HttpResponseMessage> response)
        //{
        //    // arrange
        //    proxy.Redirect(farm, server, id_secret, size)
        //        .Returns(response);
        //    var expected = await response;

        //    // act
        //    var actual = sut.ProxyImage(farm, server, id_secret, size);

        //    // assert
        //    actual.Should().Be(expected);
        //}


    }
}
