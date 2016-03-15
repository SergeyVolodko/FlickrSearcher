using System.Threading.Tasks;

namespace FlickrSearcher.Search
{
    public interface IImageRepository
    {
        Task<byte[]> GetSmallImage(
            FoundPhoto foundPhoto
            //string id, string secret, string server, string farm
            );
    }

    public class ImageRepository: IImageRepository
    {
        public async Task<byte[]> GetSmallImage(
            FoundPhoto foundPhoto)
        {
            throw new System.NotImplementedException();
        }
    }
}