using System.Net.Http;
using System.Threading.Tasks;
using FlickrSearcher.Search;
using FlickrSearcher.Search.Factories;
using FlickrSearcher.Search.Services;
using FlickrSearcher.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace FlickrSearcher.Tests.ImageProxyTests
{
    public class Redirect
    {
        [Theory]
        [AutoNSubstituteData]
        public void calls_img_url_factory_create_real_url(
            [Frozen] IImageUrlFactory factory,
            ImageProxy sut,
            int farm,
            int server,
            string idAndSecret,
            string size)
        {
            // setup
            factory
                .CreateRealImageUrl(0, 0, null, null)
                .ReturnsForAnyArgs("http://some.url");

            // act
            sut.Redirect(farm, server, idAndSecret, size);

            // assert
            factory
                .Received()
                .CreateRealImageUrl(
                    farm, server, idAndSecret, size);
        }

        [Theory]
        [AutoNSubstituteData]
        public async Task result_behavior(
            [Frozen] IImageUrlFactory factory,
            ImageProxy sut)
        {
            // setup
            var url = "https://farm2.staticflickr.com" +
                "/1460" +
                "/25750968675_5c4b5e441a_m.jpg";

            factory
                .CreateRealImageUrl(0, 0, null, null)
                .ReturnsForAnyArgs(url);
            var client = new HttpClient();
            var expected = await client.GetAsync(url);
            var expectedImage = await expected.Content.ReadAsByteArrayAsync();

            // act
            var actual = await sut.Redirect(0, 0, null, null);
            var actaulImage = await actual.Content.ReadAsByteArrayAsync();

            // assert
            actaulImage
                .ShouldBeEquivalentTo(expectedImage);
        }
    }
}
