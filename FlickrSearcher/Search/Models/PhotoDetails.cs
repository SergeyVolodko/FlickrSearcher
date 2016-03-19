using System;
using System.Runtime.Serialization;

namespace FlickrSearcher.Search.Models
{
    [DataContract]
    public class PhotoDetails
    {
        public long Id { get; set; }
        public string IconUrl { get; set; }
        public string OwnerName { get; set; }
        public string Title { get; set; }
        public DateTime? TakenDate { get; set; }
    }
}