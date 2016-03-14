using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlickrSearcher.Search;
using FlickrSearcher.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace FlickrSearcher.Tests
{
    public class SearchServiceTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void calls_photo_info_repository_search(
            [Frozen]IPhotoRepository photoRepo,
            SearchService sut,
            string text, 
            int page)
        {
            // act
            sut.Search(text, page);

            // assert
            photoRepo
                .Received()
                .Search(text, page);
        }

        [Theory]
        [AutoNSubstituteData]
        public void foreach_returned_photo_info_call_image_repository_get_small_image(
            [Frozen]IPhotoRepository photoRepo,
            [Frozen]IImageRepository imageRepo,
            SearchService sut,
            string text,
            int page,
            List<PhotoInfo> photos)
        {
            // arrange
            photoRepo
                .Search(text, page)
                .Returns(photos);

            // act
            sut.Search(text, page);

            // assert
            imageRepo
                .Received(photos.Count)
                .GetSmallImage(
                    Arg.Is<string>(id => photos.Any(p => p.Id == id)),
                    Arg.Is<string>(secret => photos.Any(p => p.Secret == secret)),
                    Arg.Is<string>(server => photos.Any(p => p.Server == server)),
                    Arg.Is<string>(farm => photos.Any(p => p.Farm == farm)));
        }

        [Theory]
        [AutoNSubstituteData]
        public void return_photos_with_id_and_image(
            [Frozen]IPhotoRepository photoRepo,
            [Frozen]IImageRepository imageRepo,
            SearchService sut,
            string text,
            int page,
            PhotoInfo photo1,
            PhotoInfo photo2,
            byte[] image1,
            byte[] image2)
        {
            // setup
            var photos = new List<PhotoInfo> {photo1, photo2};
            var expected = new List<Photo>
            {
                new Photo { Id = photo1.Id , Image = image1 },
                new Photo { Id = photo2.Id , Image = image2 }
            };

            photoRepo
                .Search(text, page)
                .Returns(photos);

            imageRepo
                .GetSmallImage(photo1.Id, photo1.Secret, photo1.Server, photo1.Farm)
                .Returns(Task.FromResult(image1));

            imageRepo
                .GetSmallImage(photo2.Id, photo2.Secret, photo2.Server, photo2.Farm)
                .Returns(Task.FromResult(image2));

            // act
            var actual = sut.Search(text, page);

            // assert
            actual
                .ShouldBeEquivalentTo(expected);
        }
        
    }
}
