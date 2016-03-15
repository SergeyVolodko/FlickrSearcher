using System;
using System.Runtime.Serialization;

namespace FlickrSearcher.Search
{
    [DataContract]
    public class FlickerPhotoDetails
    {
        public string OwnerName { get; set; }
        public string Title { get; set; }
        public DateTime? TakenDate { get; set; }
    }
}