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