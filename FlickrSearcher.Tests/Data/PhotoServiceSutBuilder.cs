using System.Collections.Generic;
using System.Threading.Tasks;
using FlickrSearcher.Search;
using NSubstitute;
using Ploeh.AutoFixture;

namespace FlickrSearcher.Tests.Data
{
    public class PhotoServiceSutData
    {
        public string InputText;

        public int InputPage;

        public PhotoService Service;

        public IFlickerEncoder FlickerEncoder;

        public IPhotoRepository PhotoRepository;

        public IImageRepository ImageRepository;

        public IList<Photo> CallSearch()
        {
            return Service.Search(InputText, InputPage);
        }
    }

    public class PhotoServiceSUTBuilder
    {
        private readonly PhotoServiceSutData data;
        private readonly Fixture fixture;

        public PhotoServiceSUTBuilder()
        {
            var photoRepo = Substitute.For<IPhotoRepository>();
            var imageRepo = Substitute.For<IImageRepository>();
            var encoder = Substitute.For<IFlickerEncoder>();
            var sut = new PhotoService(photoRepo, imageRepo, encoder);

            fixture = new Fixture();

            data = new PhotoServiceSutData
            {
                InputText = fixture.Create<string>(),
                InputPage = fixture.Create<int>(),
                PhotoRepository = photoRepo,
                ImageRepository = imageRepo,
                FlickerEncoder = encoder,

                Service = sut
            };
        }

        public PhotoServiceSutData Build()
        {
            return data;
        }

        public PhotoServiceSUTBuilder FindsPhotos(
            List<FoundPhoto> foundPhotos)
        {
            data.PhotoRepository
                .Search(data.InputText, data.InputPage)
                .Returns(foundPhotos);

            return this;
        }

        public PhotoServiceSUTBuilder GetsSmallImage(
            FoundPhoto inputFoundPhoto, 
            byte[] outputImage)
        {
            data.ImageRepository
                .GetSmallImage(inputFoundPhoto)
                .Returns(Task.FromResult(outputImage));

            return this;
        }

        public PhotoServiceSUTBuilder EncodesPhotoId(
            long inputId, 
            string outputEncodedId)
        {
            data.FlickerEncoder
                .Encode(inputId)
                .Returns(outputEncodedId);

            return this;
        }
    }
}
