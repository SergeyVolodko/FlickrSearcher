using FlickrSearcher.Search.Models;

namespace FlickrSearcher.Search.Factories
{
    public enum ImageSize
    {
        Small,
        Large,
        Icon
    }

    public interface IImageUrlFactory
    {
        string CreateImageUrl(FlickerPhoto photo, ImageSize size);
        string CreateRealImageUrl(int farm, int server, string idAndSecret, string size);
    }

    
    public class ImageUrlFactory: IImageUrlFactory
    {
        public string CreateImageUrl(
            FlickerPhoto photo,
            ImageSize size)
        {
            var imgUrl = string.Format(
                "http://localhost:62276/api/image/{0}/{1}/{2}_{3}/",
                photo.Farm,
                photo.Server,
                photo.Id,
                photo.Secret);

            return imgUrl + size.ToString().ToLower();
        }

        public string CreateRealImageUrl(
            int farm, int server, 
            string idAndSecret, 
            string size)
        {
            var sizeEnding = "_q";

            switch (size.ToLower())
            {
                case "small":
                    sizeEnding = "_q";break;
                case "large":
                    sizeEnding = "_b";break;
                case "icon":
                {
                    idAndSecret = "buddyicons/" + idAndSecret;
                    sizeEnding = "";
                    break;
                }
                default:
                    sizeEnding = "_q"; break;
            }

            return string.Format(
                "https://farm{0}.staticflickr.com" +
                     "/{1}/{2}{3}.jpg",
                     farm,
                     server,
                     idAndSecret,
                     sizeEnding);
        }
    }
}