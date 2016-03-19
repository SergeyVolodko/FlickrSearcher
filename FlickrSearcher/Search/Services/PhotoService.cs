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

            foreach (var photo in foundPhotos)
            {
                var imgUrl = urlFactory
                    .CreateImageUrl(photo, ImageSize.Small);
                var largeImgUrl = urlFactory
                    .CreateImageUrl(photo, ImageSize.Large);
                
                result.Add(new Photo
                {
                    Id = photo.Id,
                    Title = photo.Title,
                    ImageUrl = imgUrl,
                    LargeImageUrl = largeImgUrl
                });
            }

            return result;
        }

        public PhotoDetails GetPhotoDetails(long photoId)
        {
            var details = photoRepository
                .LoadPhotoDetails(photoId);
            var iconUrl = urlFactory
                .CreateImageUrl(details.OwnerPhoto, ImageSize.Icon);

            return new PhotoDetails
            {
                Id = photoId,
                IconUrl = iconUrl,
                Title = details?.Title,
                OwnerName = details?.OwnerName,
                TakenDate = details?.TakenDate
            };
        }
    }
}