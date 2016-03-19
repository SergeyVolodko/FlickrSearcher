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

        public long InputPhotoId;

        public PhotoService Service;

        public IFlickerEncoder FlickerEncoder;

        public IPhotoRepository PhotoRepository;

        public IImageRepository ImageRepository;

        public IImageUrlFactory ImageUrlFactory;

        public IList<Photo> CallSearch()
        {
            return Service.Search(InputText, InputPage);
        }

        public PhotoDetails CallGetPhotoDetails()
        {
            return Service.GetPhotoDetails(InputPhotoId);
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
            var urlFactory = Substitute.For<IImageUrlFactory>();

            var sut = new PhotoService(
                photoRepo, imageRepo, encoder, urlFactory);
            

            fixture = new Fixture();

            data = new PhotoServiceSutData
            {
                InputText = fixture.Create<string>(),
                InputPage = fixture.Create<int>(),
                InputPhotoId = fixture.Create<long>(),
                PhotoRepository = photoRepo,
                ImageRepository = imageRepo,
                FlickerEncoder = encoder,
                ImageUrlFactory = urlFactory,

                Service = sut
            };
        }

        public PhotoServiceSutData Build()
        {
            return data;
        }

        public PhotoServiceSUTBuilder FindsPhotos(
            List<FlickerPhoto> flickerPhotos)
        {
            data.PhotoRepository
                .Find(data.InputText, data.InputPage)
                .Returns(flickerPhotos);

            return this;
        }

        public PhotoServiceSUTBuilder GetsSmallImage(
            FlickerPhoto inputFlickerPhoto, 
            byte[] outputImage)
        {
            data.ImageRepository
                .GetSmallImage(inputFlickerPhoto)
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

        public PhotoServiceSUTBuilder WithInputPhotoId(
            long photoId)
        {
            data.InputPhotoId = photoId;
            return this;
        }

        public PhotoServiceSUTBuilder GetsLargeImage(
            string inputImageId, 
            byte[] outputImage)
        {
            data.ImageRepository
                .GetLargeImage(inputImageId)
                .Returns(outputImage);

            return this;
        }

        public PhotoServiceSUTBuilder LoadsPhoto(
            long inputPhotoId, 
            FlickerPhotoDetails outputFlickerDetails)
        {
            data.PhotoRepository
                .LoadPhotoDetails(inputPhotoId)
                .Returns(outputFlickerDetails);

            return this;
        }

        public PhotoServiceSUTBuilder CreatesSmallImageUrl(
            FlickerPhoto flickerPhoto, 
            string imageUrl)
        {
            data.ImageUrlFactory
                .CreateImageUrl(flickerPhoto, ImageSize.Small)
                .Returns(imageUrl);

            return this;
        }

        public PhotoServiceSUTBuilder CreatesLargeImageUrl(
            FlickerPhoto flickerPhoto, 
            string imageUrl)
        {
            data.ImageUrlFactory
                .CreateImageUrl(flickerPhoto, ImageSize.Large)
                .Returns(imageUrl);

            return this;
        }
    }
}
