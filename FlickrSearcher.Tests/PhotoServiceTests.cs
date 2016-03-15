using System.Collections.Generic;
using System.Linq;
using FlickrSearcher.Search;
using FlickrSearcher.Tests.Data;
using FlickrSearcher.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FlickrSearcher.Tests
{
    public class PhotoServiceTests
    {
        [Fact]
        public void calls_photo_info_repository_search()
        {
            // arrange
            var sutData = new PhotoServiceSUTBuilder().Build();
            var sut = sutData.Service;
            var photoRepo = sutData.PhotoRepository;
            var text = sutData.InputText;
            var page = sutData.InputPage;

            // act
            sut.Search(text, page);

            // assert
            photoRepo
                .Received()
                .Find(text, page);
        }

        [Theory]
        [AutoNSubstituteData]
        public void foreach_found_photo_calls_image_repository_get_small_image(
            List<FoundPhoto> photos)
        {
            // arrange
            var sut = new PhotoServiceSUTBuilder()
                .FindsPhotos(photos)
                .Build();

            // act
            sut.CallSearch();

            // assert
            sut.ImageRepository
                .Received(photos.Count)
                .GetSmallImage(
                    Arg.Is<FoundPhoto>(
                        p => photos.Contains(p)));
        }

        [Theory]
        [AutoNSubstituteData]
        public void foreach_found_photo_id_calls_flicker_encoder_encode(
            List<FoundPhoto> photos)
        {
            // arrange
            var sut = new PhotoServiceSUTBuilder()
                .FindsPhotos(photos)
                .Build();

            // act
            sut.CallSearch();

            // assert
            sut.FlickerEncoder
                .Received(photos.Count)
                .Encode(
                    Arg.Is<long>(
                        id => photos.Any(p => p.Id == id)));
        }

        [Theory]
        [AutoNSubstituteData]
        public void returns_photos_with_image_and_encoded_id(
            FoundPhoto photo1,
            FoundPhoto photo2,
            byte[] image1,
            byte[] image2,
            string encodedId1,
            string encodedId2)
        {
            // setup
            var photos = new List<FoundPhoto> { photo1, photo2 };

            var sut = new PhotoServiceSUTBuilder()
               .FindsPhotos(photos)
               .GetsSmallImage(photo1, image1)
               .GetsSmallImage(photo2, image2)
               .EncodesPhotoId(photo1.Id, encodedId1)
               .EncodesPhotoId(photo2.Id, encodedId2)
               .Build();

            var expected = new List<Photo>
            {
                new Photo { Id = encodedId1, Image = image1 },
                new Photo { Id = encodedId2, Image = image2 }
            };

            // act
            var actual = sut.CallSearch();

            // assert
            actual
                .ShouldBeEquivalentTo(expected);
        }
    }
}
