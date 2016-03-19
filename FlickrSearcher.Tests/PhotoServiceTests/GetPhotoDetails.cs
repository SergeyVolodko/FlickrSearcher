using FlickrSearcher.Search.Factories;
using FlickrSearcher.Search.Models;
using FlickrSearcher.Tests.Data;
using FlickrSearcher.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FlickrSearcher.Tests.PhotoServiceTests
{
    public class GetPhotoDetails
    {
        [Fact]
        public void calls_photo_repository_load_details()
        {
            // arrange
            var sutData = new PhotoServiceSUTBuilder()
                .LoadsSomePhotoDetails()
                .Build();
            var sut = sutData.Service;
            var photoRepo = sutData.PhotoRepository;
            var id = sutData.InputPhotoId;

            // act
            sut.GetPhotoDetails(id);

            // assert
            photoRepo
                .Received()
                .LoadPhotoDetails(id);
        }


        [Theory]
        [AutoNSubstituteData]
        public void calls_image_url_factory_create_icon_image(
           FlickerPhotoDetails details,
           long photoId)
        {
            // arrange
            var sut = new PhotoServiceSUTBuilder()
                .WithInputPhotoId(photoId)
                .LoadsPhotoDetails(details)
                .Build();

            // act
            sut.CallGetPhotoDetails();

            // assert
            sut.ImageUrlFactory
                .Received()
                .CreateImageUrl(details.OwnerPhoto, ImageSize.Icon);
        }

        [Theory]
        [AutoNSubstituteData]
        public void returns_photo_details_with_large_image(
            FlickerPhotoDetails details,
            string iconUrl)
        {
            // arrange
            var sut = new PhotoServiceSUTBuilder()
                .LoadsPhotoDetails(details)
                .CreatesIconUrl(details.OwnerPhoto, iconUrl)
                .Build();

            details.PhotoId = sut.InputPhotoId;

            var expected = new PhotoDetails
            {
                Id = details.PhotoId,
                IconUrl = iconUrl,
                Title = details.Title,
                OwnerName = details.OwnerName,
                TakenDate = details.TakenDate
            };

            // act
            var actual = sut.CallGetPhotoDetails();

            // assert
            actual
                .ShouldBeEquivalentTo(expected);
        }


    }
}
