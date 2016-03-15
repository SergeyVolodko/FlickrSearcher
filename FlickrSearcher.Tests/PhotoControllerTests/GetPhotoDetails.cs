using System.Net.Http;
using System.Web.Http;
using FlickrSearcher.Search;
using FlickrSearcher.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace FlickrSearcher.Tests.PhotoControllerTests
{
    public class GetPhotoDetails
    {
        [Fact]
        public void routing()
        {
            // arrange
            var uri = @"http://localhost:62276/api/photoDetails/42";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var config = new HttpConfiguration();

            // act
            WebApiConfig.Register(config);
            var route = WebApi.RouteRequest(config, request);

            // asserts
            route.Controller.Should().Be<PhotoController>();
            route.Action.Should().Be("GetPhotoDetails");
        }

        [Theory]
        [ControllerAutoData]
        public void calls_photo_service_get_details(
           [Frozen]IPhotoService service,
           PhotoController sut,
           long photoId)
        {
            // act
            sut.GetPhotoDetails(photoId);

            // assert
            service
                .Received()
                .GetPhotoDetails(photoId);
        }

        [Theory]
        [ControllerAutoData]
        public void returns_photo_details_returned_by_service(
            [Frozen]IPhotoService service,
            PhotoController sut,
            long photoId,
            PhotoDetails photoDetails)
        {
            // arrange
            service
                .GetPhotoDetails(photoId)
                .Returns(photoDetails);

            // act
            var actual = sut.GetPhotoDetails(photoId);

            // assert
            actual
                .ShouldBeEquivalentTo(photoDetails);
        }
    }
}
