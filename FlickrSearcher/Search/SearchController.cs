using System.Web.Http;

namespace FlickrSearcher.Search
{
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Search()
        {
            return "Ok";
        }
    }
}
