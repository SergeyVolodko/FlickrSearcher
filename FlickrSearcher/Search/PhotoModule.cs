using Autofac;

namespace FlickrSearcher.Search
{
    public class PhotoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PhotoRepository>()
                .As<IPhotoRepository>();

            builder.RegisterType<ImageRepository>()
                .As<IImageRepository>();

            builder.RegisterType<FlickerEncoder>()
                .As<IFlickerEncoder>();

            builder.RegisterType<PhotoService>()
                .As<IPhotoService>();

            builder.RegisterType<PhotoController>()
                .AsSelf();
        }
    }
}