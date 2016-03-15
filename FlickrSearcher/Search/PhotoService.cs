using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlickrSearcher.Search
{
    public interface IPhotoService
    {
        IList<Photo> Search(string text, int page);
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
                var encodedId = encoder.Encode(foundPhoto.Id);
                var photo = new Photo { Id = encodedId };

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
    }
}