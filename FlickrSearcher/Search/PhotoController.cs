using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FlickrSearcher.Search
{
    [RoutePrefix("api")]
    public class PhotoController : ApiController
    {
        private readonly IPhotoService photoService;

        public PhotoController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        [HttpGet]
        [Route("search")]
        public IList<Photo> Search(string text, int page)
        {
            return photoService.Search(text, page);
        }

        [HttpGet]
        [Route("photoDetails/{id}")]
        public PhotoDetails GetPhotoDetails(long id)
        {
            return photoService.GetPhotoDetails(id);
        }

        [HttpGet]
        [Route("proxy/{farm}/{server}/{id_secret}")]
        public async Task<HttpResponseMessage> Proxy(
            string farm,
            string server,
            string id_secret)
        {
            var url = string.Format(
                "https://farm{0}.staticflickr.com" +
                "/{1}/{2}_q.jpg", farm,
                server,
                id_secret);

            var httpClient = new HttpClient();
            return await httpClient.GetAsync(url);
        }
    }
}
