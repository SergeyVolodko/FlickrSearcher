using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using FlickrSearcher.Search;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FlickrSearcher.Startup))]
namespace FlickrSearcher
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config = WebApiConfig.ConfigureRoutes(config);

            var builder = new ContainerBuilder();

            builder.RegisterModule(new PhotoModule());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            
            app.UseWebApi(config);
        }
    }
}