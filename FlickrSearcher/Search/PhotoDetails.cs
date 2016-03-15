using System;
using System.Runtime.Serialization;

namespace FlickrSearcher.Search
{
    [DataContract]
    public class PhotoDetails
    {
        public long Id { get; set; }
        public byte[] Image { get; set; }
        public string OwnerName { get; set; }
        public string Title { get; set; }
        public DateTime? TakenDate { get; set; }
    }
}