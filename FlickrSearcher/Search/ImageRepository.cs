using System.Net.Http;
using System.Threading.Tasks;

namespace FlickrSearcher.Search
{
    public interface IImageRepository
    {
        Task<byte[]> GetSmallImage(
            FlickerPhoto flickerPhoto
            //string id, string secret, string server, string farm
            );

        byte[] GetLargeImage(string imageId);
    }

    public class ImageRepository: IImageRepository
    {
        public Task<byte[]> GetSmallImage(
            FlickerPhoto flickerPhoto)
        {
            var httpClient = new HttpClient();

            var url = string.Format(
                "https://farm{0}.staticflickr.com" +
                     "/{1}/{2}_{3}_q.jpg",
                flickerPhoto.Farm,
                flickerPhoto.Server,
                flickerPhoto.Id,
                flickerPhoto.Secret);

            var response = httpClient.GetAsync(url)
                .GetAwaiter().GetResult();

            return response.Content.ReadAsByteArrayAsync();
        }

        public byte[] GetLargeImage(string imageId)
        {
            var httpClient = new HttpClient();

            var url = "https://flic.kr/p/" + imageId;
            
            var response = httpClient.GetAsync(url)
                .GetAwaiter().GetResult();

            byte[] image = response.Content
                .ReadAsByteArrayAsync()
                .GetAwaiter().GetResult();

            return image;
        }
    }
}