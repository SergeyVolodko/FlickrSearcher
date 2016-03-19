using FlickrSearcher.Search.Factories;
using FluentAssertions;
using Xunit;

namespace FlickrSearcher.Tests.ImageUrlFactoryTests
{
    public class CreateRealImageUrl
    {
        private const string urlSmall = "https://farm2.staticflickr.com" +
                            "/1460/25750968675_5c4b5e441a_" +
                            "q.jpg";
        private const string urlLarge = "https://farm2.staticflickr.com" +
                            "/1460/25750968675_5c4b5e441a_" +
                            "b.jpg";
        private const string urlIcon = "https://farm4.staticflickr.com/" +
                            "3890/buddyicons/" +
                            "125349441@N03.jpg";

        [Theory]
        [InlineData(2, 1460, "25750968675_5c4b5e441a", "small", urlSmall)]
        [InlineData(2, 1460, "25750968675_5c4b5e441a", "large", urlLarge)]
        [InlineData(4, 3890, "125349441@N03", "icon", urlIcon)]
        [InlineData(2, 1460, "25750968675_5c4b5e441a", "unkonwn", urlSmall)]

        public void create_image_url_of_size(
            int farm,
            int server,
            string idAndSecret,
            string size,
            string expectedUrl)
        {
            // arrange
            var sut = new ImageUrlFactory();


            // act // assert
            sut.CreateRealImageUrl(farm, server, idAndSecret, size)
                .Should()
                .Be(expectedUrl);
        }
    }
}
