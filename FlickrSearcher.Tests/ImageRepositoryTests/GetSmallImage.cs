using System.Threading.Tasks;
using FlickrSearcher.Search;
using FluentAssertions;
using Xunit;

namespace FlickrSearcher.Tests.ImageRepositoryTests
{
    public class GetSmallImage
    {
        [Fact]
        public async Task integration()
        {
            // arrange
            var foundPhoto = new FlickerPhoto
            {
                Id = 25565832916,
                Secret = "8a39692507",
                Farm = 2,
                Server = "1587"
            };
            var sut = new ImageRepository();

            // act
            var actual = await sut.GetSmallImage(foundPhoto);
            
            // assert
            actual.Length
                .Should()
                .Be(26207);
        }
    }
}
