using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FlickrSearcher.Search.Models;
using FlickrSearcher.Search.Services;

namespace FlickrSearcher.Search
{
    [RoutePrefix("api")]
    public class PhotoController : ApiController
    {
        private readonly IPhotoService photoService;
        private readonly IImageProxy proxy;

        public PhotoController(
            IPhotoService photoService,
            IImageProxy proxy)
        {
            this.photoService = photoService;
            this.proxy = proxy;
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
        [Route("image/{farm}/{server}/{id_secret}/{size}")]
        public async Task<HttpResponseMessage> ProxyImage(
            int farm,
            int server,
            string id_secret,
            string size)
        {
            return await proxy
                .Redirect(farm, server, id_secret, size);
        }
    }
}
