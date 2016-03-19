using System.Net.Http;
using System.Threading.Tasks;
using FlickrSearcher.Search.Factories;

namespace FlickrSearcher.Search.Services
{
    public interface IImageProxy
    {
        Task<HttpResponseMessage> Redirect(int farm, int server, string idAndSecret, string size);
    }
    
    public class ImageProxy: IImageProxy
    {
        private readonly IImageUrlFactory urlFactory;

        public ImageProxy(IImageUrlFactory urlFactory)
        {
            this.urlFactory = urlFactory;
        }

        public Task<HttpResponseMessage> Redirect(
            int farm, 
            int server, 
            string idAndSecret, 
            string size)
        {
            var imgUrl = urlFactory.CreateRealImageUrl(
                    farm, server, idAndSecret, size);

            var httpClient = new HttpClient();

            return httpClient.GetAsync(imgUrl);
        }
    }
}