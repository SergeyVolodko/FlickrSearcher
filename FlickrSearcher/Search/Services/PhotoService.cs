using System.Collections.Generic;
using FlickrSearcher.Search.Factories;
using FlickrSearcher.Search.Models;
using FlickrSearcher.Search.Repoitories;

namespace FlickrSearcher.Search.Services
{
    public interface IPhotoService
    {
        IList<Photo> Search(string text, int page);
        PhotoDetails GetPhotoDetails(long photoId);
    }

    public class PhotoService: IPhotoService
    {
        private readonly IPhotoRepository photoRepository;
        private IImageUrlFactory urlFactory;
        
        public PhotoService(
            IPhotoRepository photoRepository,
            IImageUrlFactory urlFactory)
        {
            this.photoRepository = photoRepository;
            this.urlFactory = urlFactory;
        }

        public IList<Photo> Search(string text, int page)
        {
            var foundPhotos = photoRepository.Find(text, page);

            var result = new List<Photo>();

            foreach (var foundPhoto in foundPhotos)
            {
                var imgUrl = urlFactory
                    .CreateImageUrl(foundPhoto, ImageSize.Small);
                var largeImgUrl = urlFactory
                    .CreateImageUrl(foundPhoto, ImageSize.Large);


                var photo = new Photo
                {
                    Id = foundPhoto.Id,
                    Title = foundPhoto.Title,
                    ImageUrl = imgUrl,
                    LargeImageUrl = largeImgUrl
                };

                result.Add(photo);
            }

            return result;
        }

        public PhotoDetails GetPhotoDetails(long photoId)
        {
            return null;
            //var details = photoRepository.LoadPhotoDetails(photoId);
            //var imageId = encoder.Encode(photoId);
            //var image = imageRepository.GetLargeImage(imageId);

            //return new PhotoDetails
            //{
            //    Id = photoId,
            //    Image = image,
            //    Title = details?.Title,
            //    OwnerName = details?.OwnerName,
            //    TakenDate = details?.TakenDate
            //};
        }
    }
}