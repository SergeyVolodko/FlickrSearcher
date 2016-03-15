using FlickrSearcher.Search;
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

        [Fact]
        public void calls_flicker_encoder_encode_photo_id()
        {
            // arrange
            var sut = new PhotoServiceSUTBuilder().Build();
            var photoId = sut.InputPhotoId;

            // act
            sut.CallGetPhotoDetails();

            // assert
            sut.FlickerEncoder
                .Received()
                .Encode(photoId);
        }

        [Theory]
        [AutoNSubstituteData]
        public void calls_image_repository_get_large_image_with_encoded_photo_id(
            long photoId,
            string imageId)
        {
            // arrange
            var sut = new PhotoServiceSUTBuilder()
                .WithInputPhotoId(photoId)
                .EncodesPhotoId(photoId, imageId)
                .Build();
            
            // act
            sut.CallGetPhotoDetails();

            // assert
            sut.ImageRepository
                .Received()
                .GetLargeImage(imageId);
        }

        [Theory]
        [AutoNSubstituteData]
        public void returns_photo_details_with_large_image(
            long photoId,
            FlickerPhotoDetails details,
            string imageId,
            byte[] image)
        {
            // arrange
            var sut = new PhotoServiceSUTBuilder()
                .WithInputPhotoId(photoId)
                .LoadsPhoto(photoId, details)
                .EncodesPhotoId(photoId, imageId)
                .GetsLargeImage(imageId, image)
                .Build();
            
            var expected = new PhotoDetails
            {
                Id = photoId,
                Image = image,
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
