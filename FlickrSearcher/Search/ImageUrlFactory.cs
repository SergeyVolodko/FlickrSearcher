namespace FlickrSearcher.Search
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

            return imgUrl + size;
        }

        public string CreateRealImageUrl(
            int farm, int server, 
            string idAndSecret, 
            string size)
        {
            var sizeEnding = "q";

            switch (size.ToLower())
            {
                case "small":
                    sizeEnding = "q";break;
                case "large":
                    sizeEnding = "b";break;
                case "icon":
                    sizeEnding = "s";break;
                default:
                    sizeEnding = "q"; break;
            }

            return string.Format(
                "https://farm{0}.staticflickr.com" +
                     "/{1}/{2}_{3}.jpg",
                     farm,
                     server,
                     idAndSecret,
                     sizeEnding);
        }
    }
}