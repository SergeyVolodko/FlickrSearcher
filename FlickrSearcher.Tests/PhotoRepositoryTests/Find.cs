using System.Linq;
using FlickrSearcher.Search.Repoitories;
using FluentAssertions;
using Xunit;

namespace FlickrSearcher.Tests.PhotoRepositoryTests
{
    public class Find
    {
        [Fact]
        public void returns_10_items()
        {
            // arrange
            var sut = new PhotoRepository();

            // act
            var actual = sut.Find("Igor Nikolaev", 1);

            // assert
            actual
                .Count
                .Should().Be(10);
        }

        [Fact]
        public void item_manual_approval()
        {
            // arrange
            var sut = new PhotoRepository();

            // act
            var actual = sut.Find("Photo", 1).First();

            // assert
            var id = long.Parse(actual.Id);
            id.Should()
                .BeGreaterThan(0);

            actual.Secret
                .Should()
                .NotBeNullOrEmpty();

            actual.Farm
                .Should()
                .BeGreaterThan(0);

            actual.Server
                .Should()
                .BeGreaterThan(0);

            actual.Title
                .Should()
                .BeOfType<string>();
                
        }
    }
}
