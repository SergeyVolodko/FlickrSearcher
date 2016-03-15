using System.Runtime.Serialization;

namespace FlickrSearcher.Search
{
    [DataContract]
    public class Photo
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "image")]
        public byte[] Image { get; set; }
    }
}