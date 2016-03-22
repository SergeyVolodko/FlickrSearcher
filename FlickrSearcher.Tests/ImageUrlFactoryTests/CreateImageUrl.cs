using FlickrSearcher.Search.Factories;
using FlickrSearcher.Search.Models;
using FluentAssertions;
using Ploeh.AutoFixture;
using Xunit;

namespace FlickrSearcher.Tests.ImageUrlFactoryTests
{
    public class CreateImageUrl
    {
        private FlickerPhoto photo;
        private ImageUrlFactory sut;

        public CreateImageUrl()
        {
            photo = (new Fixture()).Create<FlickerPhoto>();
            sut = new ImageUrlFactory();
        }

        [Theory]
        [InlineData(ImageSize.Large, "{0}_{1}", "large")]
        [InlineData(ImageSize.Small, "{0}_{1}", "small")]
        [InlineData(ImageSize.Icon, "{0}", "icon")]
        public void create_image_url_of_size(
            ImageSize size,
            string idPattern,
            string expectedUrlEnding)
        {
            // arrange
            var expectedImageId = string.Format(idPattern, photo.Id, photo.Secret);

            var expected = string.Format(
                "http://localhost:62276/api/image/{0}/{1}/{2}/{3}",
                photo.Farm,
                photo.Server,
                expectedImageId,
                expectedUrlEnding);

            // act // assert
            sut.CreateImageUrl(photo, size)
                .Should()
                .Be(expected);
        }
    }
}
