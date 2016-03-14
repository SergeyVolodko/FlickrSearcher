using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlickrSearcher.Search
{
    public interface ISearchService
    {
        IList<Photo> Search(string text, int page);
    }

    public class SearchService: ISearchService
    {
        private readonly IPhotoRepository photoRepository;
        private readonly IImageRepository imageRepository;

        public SearchService(
            IPhotoRepository photoRepository,
            IImageRepository imageRepository)
        {
            this.photoRepository = photoRepository;
            this.imageRepository = imageRepository;
        }

        public IList<Photo> Search(string text, int page)
        {
            var infos = photoRepository.Search(text, page);

            var tasks = new List<Task<byte[]>>();
            var result = new List<Photo>();

            foreach (var info in infos)
            {
                var photo = new Photo { Id = info.Id };
                var task = imageRepository
                        .GetSmallImage(
                            info.Id,info.Secret,
                            info.Server,info.Farm);

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