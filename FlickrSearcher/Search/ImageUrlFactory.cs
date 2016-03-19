using System;

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

        //public string CreateSmallImageUrl(
        //    FlickerPhoto photo)
        //{
        //    var imgUrl = CreateImageUrl(photo);

        //    var size = ImageSize.Small.ToString().ToLower();

        //    return imgUrl + size;
        //}


        //public string CreateLargeImageUrl(FlickerPhoto photo)
        //{
        //    var imgUrl = CreateImageUrl(photo);

        //    return imgUrl + "large";
        //}
    }
}