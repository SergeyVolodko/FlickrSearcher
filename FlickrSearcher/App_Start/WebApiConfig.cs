using System.Net.Http.Formatting;
using System.Web.Http;

namespace FlickrSearcher
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ConfigureRoutes(config);
        }

        public static HttpConfiguration ConfigureRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            
            config.EnsureInitialized();

            return config;
        }
    }
}