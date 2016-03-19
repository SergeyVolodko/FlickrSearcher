using System.Collections.Generic;
using System.Linq;
using FlickrSearcher.Search;
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

        //[Theory]
        //[AutoNSubstituteData]
        //public void foreach_found_photo_calls_image_repository_get_small_image(
        //    List<FlickerPhoto> photos)
        //{
        //    // arrange
        //    var sut = new PhotoServiceSUTBuilder()
        //        .FindsPhotos(photos)
        //        .Build();

        //    // act
        //    sut.CallSearch();

        //    // assert
        //    sut.ImageRepository
        //        .Received(photos.Count)
        //        .GetSmallImage(
        //            Arg.Is<FlickerPhoto>(
        //                p => photos.Contains(p)));
        //}

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


        //[Theory]
        //[AutoNSubstituteData]
        //public void returns_photos_with_image_photo_id_and_title(
        //    FlickerPhoto photo1,
        //    FlickerPhoto photo2,
        //    byte[] image1,
        //    byte[] image2)
        //{
        //    setup
        //   var photos = new List<FlickerPhoto> { photo1, photo2 };

        //    var sut = new PhotoServiceSUTBuilder()
        //       .FindsPhotos(photos)
        //       .GetsSmallImage(photo1, image1)
        //       .GetsSmallImage(photo2, image2)
        //       .Build();

        //    var expected = new List<Photo>
        //    {
        //        new Photo { Id = photo1.Id, Image = image1, Title = photo1.Title },
        //        new Photo { Id = photo2.Id, Image = image2, Title = photo2.Title }
        //    };

        //    act
        //   var actual = sut.CallSearch();

        //    assert
        //    actual
        //        .ShouldBeEquivalentTo(expected);
        //}



        [Theory]
        [AutoNSubstituteData]
        public void returns_photos_with_photo_id_title_and_img_urls(
            FlickerPhoto photo1,
            FlickerPhoto photo2,
            string smallImgUrl1,
            string smallImgUrl2,
            string largeImgUrl1,
            string largeImgUrl2)
        {
            // arrange
            var photos = new List<FlickerPhoto> { photo1, photo2 };

            var sut = new PhotoServiceSUTBuilder()
               .FindsPhotos(photos)
               .CreatesSmallImageUrl(photo1, smallImgUrl1)
               .CreatesSmallImageUrl(photo2, smallImgUrl2)
               .CreatesLargeImageUrl(photo1, largeImgUrl1)
               .CreatesLargeImageUrl(photo2, largeImgUrl2)
               .Build();

            var expected = new List<Photo>
            {
                new Photo {
                    Id = photo1.Id,
                    ImageUrl = smallImgUrl1,
                    LargeImageUrl = largeImgUrl1,
                    Title = photo1.Title },
                new Photo {
                    Id = photo2.Id,
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
