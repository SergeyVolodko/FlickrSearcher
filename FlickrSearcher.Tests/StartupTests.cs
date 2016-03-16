using Autofac;
using FlickrSearcher.Search;
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
        public void repositories_registered()
        {
            var photoRepo = container.Resolve<IPhotoRepository>();
            var imageRepo = container.Resolve<IImageRepository>();

            photoRepo.Should().BeOfType<PhotoRepository>();
            imageRepo.Should().BeOfType<ImageRepository>();
        }

        [Fact]
        public void service_registered()
        {
            var encoder = container.Resolve<IFlickerEncoder>();
            var service = container.Resolve<IPhotoService>();

            encoder.Should().BeOfType<FlickerEncoder>();
            service.Should().BeOfType<PhotoService>();
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
