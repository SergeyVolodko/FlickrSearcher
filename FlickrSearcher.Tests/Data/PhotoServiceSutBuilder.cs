using System.Collections.Generic;
using FlickrSearcher.Search.Factories;
using FlickrSearcher.Search.Models;
using FlickrSearcher.Search.Repoitories;
using FlickrSearcher.Search.Services;
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

        public IPhotoRepository PhotoRepository;

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
            var urlFactory = Substitute.For<IImageUrlFactory>();

            var sut = new PhotoService(photoRepo, urlFactory);
            

            fixture = new Fixture();

            data = new PhotoServiceSutData
            {
                InputText = fixture.Create<string>(),
                InputPage = fixture.Create<int>(),
                InputPhotoId = fixture.Create<long>(),
                PhotoRepository = photoRepo,
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
            flickerPhotos.ForEach(p=> p.Id = fixture.Create<long>().ToString());

            data.PhotoRepository
                .Find(data.InputText, data.InputPage)
                .Returns(flickerPhotos);

            return this;
        }


        public PhotoServiceSUTBuilder FindsSpecificPhotos(
            List<FlickerPhoto> flickerPhotos)
        {
            data.PhotoRepository
                .Find(data.InputText, data.InputPage)
                .Returns(flickerPhotos);

            return this;
        }

        public PhotoServiceSUTBuilder WithInputPhotoId(
            long photoId)
        {
            data.InputPhotoId = photoId;
            return this;
        }

        public PhotoServiceSUTBuilder LoadsPhotoDetails(
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

        public PhotoServiceSUTBuilder CreatesIconUrl(
            FlickerPhoto inputPhoto,
            string outputIconUrl)
        {
            data.ImageUrlFactory
                .CreateImageUrl(inputPhoto, ImageSize.Icon)
                .Returns(outputIconUrl);

            return this;
        }

        public PhotoServiceSUTBuilder LoadsPhotoDetails(
            FlickerPhotoDetails details)
        {
            LoadsPhotoDetails(
                data.InputPhotoId,
                details);

            return this;
        }


        public PhotoServiceSUTBuilder LoadsSomePhotoDetails()
        {
            LoadsPhotoDetails(fixture.Create<FlickerPhotoDetails>());

            return this;
        }

    }
}
