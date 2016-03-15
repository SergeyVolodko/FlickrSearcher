using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlickrSearcher.Search
{
    public interface IPhotoService
    {
        IList<Photo> Search(string text, int page);
        PhotoDetails GetPhotoDetails(long photoId);
    }

    public class PhotoService: IPhotoService
    {
        private readonly IPhotoRepository photoRepository;
        private readonly IImageRepository imageRepository;
        private readonly IFlickerEncoder encoder;

        public PhotoService(
            IPhotoRepository photoRepository,
            IImageRepository imageRepository,
            IFlickerEncoder encoder)
        {
            this.photoRepository = photoRepository;
            this.imageRepository = imageRepository;
            this.encoder = encoder;
        }

        public IList<Photo> Search(string text, int page)
        {
            var foundPhotos = photoRepository.Find(text, page);

            var result = new List<Photo>();
            var tasks = new List<Task<byte[]>>();

            foreach (var foundPhoto in foundPhotos)
            {
                var photo = new Photo
                {
                    Id = foundPhoto.Id,
                    Title = foundPhoto.Title
                };

                var task = imageRepository.GetSmallImage(foundPhoto);
                task.ContinueWith(t =>
                {
                    photo.Image = t.Result;
                });

                result.Add(photo);
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());

            return result;
        }

        public PhotoDetails GetPhotoDetails(long photoId)
        {
            var details = photoRepository.LoadPhotoDetails(photoId);
            var imageId = encoder.Encode(photoId);
            var image = imageRepository.GetLargeImage(imageId);

            return new PhotoDetails
            {
                Id = photoId,
                Image = image,
                Title = details?.Title,
                OwnerName = details?.OwnerName,
                TakenDate = details?.TakenDate
            };
        }
    }
}