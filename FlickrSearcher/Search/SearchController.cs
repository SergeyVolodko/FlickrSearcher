using System.Collections.Generic;
using System.Web.Http;

namespace FlickrSearcher.Search
{
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        [Route("")]
        public IList<Photo> Search(string text, int page)
        {
            return searchService.Search(text, page);
        }
    }
}
