using System.Collections.Generic;
using FlickrSearcher.Search.Factories;
using FlickrSearcher.Search.Models;
using FlickrSearcher.Tests.Data;
using FlickrSearcher.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FlickrSearcher.Tests.PhotoServiceTests
{
    public class Search
    {
        [Fact]
        public void calls_photo_info_repository_find()
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
        public void foreach_found_photo_calls_image_url_factory_create_small_image(
            List<FlickerPhoto> photos)
        {
            // arrange
            var sut = new PhotoServiceSUTBuilder()
                .FindsPhotos(photos)
                .Build();

            // act
            sut.CallSearch();

            // assert
            sut.ImageUrlFactory
                .Received(photos.Count)
                .CreateImageUrl(
                    Arg.Is<FlickerPhoto>(p => photos.Contains(p)),
                    ImageSize.Small);
        }

        [Theory]
        [AutoNSubstituteData]
        public void foreach_found_photo_calls_image_url_factory_create_large_image(
            List<FlickerPhoto> photos)
        {
            // arrange
            var sut = new PhotoServiceSUTBuilder()
                .FindsPhotos(photos)
                .Build();

            // act
            sut.CallSearch();

            // assert
            sut.ImageUrlFactory
                .Received(photos.Count)
                .CreateImageUrl(
                    Arg.Is<FlickerPhoto>(p => photos.Contains(p)),
                    ImageSize.Large);
        }
        
        [Theory]
        [AutoNSubstituteData]
        public void returns_photos_with_photo_id_title_and_img_urls(
            long photo1Id,
            long photo2Id,
            FlickerPhoto photo1,
            FlickerPhoto photo2,
            string smallImgUrl1,
            string smallImgUrl2,
            string largeImgUrl1,
            string largeImgUrl2)
        {
            // arrange
            photo1.Id = photo1Id.ToString();
            photo2.Id = photo2Id.ToString();
            var photos = new List<FlickerPhoto> { photo1, photo2 };

            var sut = new PhotoServiceSUTBuilder()
               .FindsSpecificPhotos(photos)
               .CreatesSmallImageUrl(photo1, smallImgUrl1)
               .CreatesSmallImageUrl(photo2, smallImgUrl2)
               .CreatesLargeImageUrl(photo1, largeImgUrl1)
               .CreatesLargeImageUrl(photo2, largeImgUrl2)
               .Build();

            var expected = new List<Photo>
            {
                new Photo {
                    Id = photo1Id,
                    ImageUrl = smallImgUrl1,
                    LargeImageUrl = largeImgUrl1,
                    Title = photo1.Title },
                new Photo {
                    Id = photo2Id,
                    ImageUrl = smallImgUrl2,
                    LargeImageUrl = largeImgUrl2,
                    Title = photo2.Title }
            };

            // act
            var actual = sut.CallSearch();

            // assert
            actual
                .ShouldBeEquivalentTo(expected);
        }
    }
}
