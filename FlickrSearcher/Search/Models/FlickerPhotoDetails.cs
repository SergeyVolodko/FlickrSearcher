using System;
using System.Runtime.Serialization;

namespace FlickrSearcher.Search.Models
{
    [DataContract]
    public class FlickerPhotoDetails
    {
        [DataMember(Name = "id")]
        public string PhotoId { get; set; }

        [DataMember(Name = "owner_name")]
        public string OwnerName { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "taken_date")]
        public DateTime? TakenDate { get; set; }
    }
}