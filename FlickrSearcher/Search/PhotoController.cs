using System.Collections.Generic;
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
    }
}
