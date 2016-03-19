using Autofac;
using FlickrSearcher.Search;
using FlickrSearcher.Search.Factories;
using FlickrSearcher.Search.Repoitories;
using FlickrSearcher.Search.Services;
using FluentAssertions;
using Xunit;

namespace FlickrSearcher.Tests
{
    public class StartupTests
    {
        private IContainer container;

        public StartupTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new PhotoModule());
            container = builder.Build();
        }

        [Fact]
        public void repository_and_factory_registered()
        {
            var photoRepo = container.Resolve<IPhotoRepository>();
            var factory = container.Resolve<IImageUrlFactory>();

            photoRepo.Should().BeOfType<PhotoRepository>();
            factory.Should().BeOfType<ImageUrlFactory>();
        }

        [Fact]
        public void services_registered()
        {
            var service = container.Resolve<IPhotoService>();
            var proxy = container.Resolve<IImageProxy>();
            
            service.Should().BeOfType<PhotoService>();
            proxy.Should().BeOfType<ImageProxy>();
        }

        [Fact]
        public void controller_registered()
        {
            container.Resolve<PhotoController>()
                .Should()
                .NotBeNull();
        }
    }
}
