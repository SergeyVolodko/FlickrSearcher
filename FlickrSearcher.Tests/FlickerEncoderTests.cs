using FlickrSearcher.Search;
using FluentAssertions;
using Xunit;

namespace FlickrSearcher.Tests
{
    public class FlickerEncoderTests
    {
        [Fact]
        public void encode()
        {
            // arrange
            var sut = new FlickerEncoder();
            var input = 25750968675;
            var expected = "FewrQx";

            // act 
            var actual = sut.Encode(input);

            // assert
            actual.Should().Be(expected);
        }
    }
}
