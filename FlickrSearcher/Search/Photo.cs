using System.Runtime.Serialization;

namespace FlickrSearcher.Search
{
    [DataContract]
    public class Photo
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        //[DataMember(Name = "image")]
        //public byte[] Image { get; set; }

        [DataMember(Name = "image_url")]
        public string ImageUrl { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "large_image_url")]
        public string LargeImageUrl { get; set; }
    }
}