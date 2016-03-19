using System.Runtime.Serialization;

namespace FlickrSearcher.Search.Models
{
    [DataContract]
    public class FlickerPhoto
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "secret")]
        public string Secret { get; set; }

        [DataMember(Name = "server")]
        public string Server { get; set; }

        [DataMember(Name = "farm")]
        public int Farm { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}