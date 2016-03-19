using Autofac;
using FlickrSearcher.Search.Factories;
using FlickrSearcher.Search.Repoitories;
using FlickrSearcher.Search.Services;

namespace FlickrSearcher.Search
{
    public class PhotoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PhotoRepository>()
                .As<IPhotoRepository>();

            builder.RegisterType<ImageUrlFactory>()
                .As<IImageUrlFactory>();

            builder.RegisterType<PhotoService>()
                .As<IPhotoService>();

            builder.RegisterType<ImageProxy>()
                .As<IImageProxy>();

            builder.RegisterType<PhotoController>()
                .AsSelf();
        }
    }
}