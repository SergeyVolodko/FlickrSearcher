using System.Net.Http;
using System.Threading.Tasks;

namespace FlickrSearcher.Search
{
    public interface IImageRepository
    {
        Task<byte[]> GetSmallImage(
            FoundPhoto foundPhoto
            //string id, string secret, string server, string farm
            );
    }

    public class ImageRepository: IImageRepository
    {
        public Task<byte[]> GetSmallImage(
            FoundPhoto foundPhoto)
        {
            var httpClient = new HttpClient();

            var url = string.Format(
                "https://farm{0}.staticflickr.com" +
                     "/{1}/{2}_{3}_m.jpg",
                foundPhoto.Farm,
                foundPhoto.Server,
                foundPhoto.Id,
                foundPhoto.Secret);

            var response = httpClient.GetAsync(url)
                .GetAwaiter().GetResult();

            return response.Content.ReadAsByteArrayAsync();
        }
    }
}