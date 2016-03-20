using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FlickrSearcher.Search.Models
{
    [DataContract]
    public class FlickerPhotoDetails
    {
        [DataMember(Name = "id")]
        public long PhotoId { get; set; }

        [DataMember(Name = "owner_username")]
        public string OwnerUserName { get; set; }

        [DataMember(Name = "owner_realname")]
        public string OwnerRealName { get; set; }

        [DataMember(Name = "owner_location")]
        public string OwnerLocation { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "taken_date")]
        public DateTime? TakenDate { get; set; }

        [DataMember(Name = "owner")]
        public FlickerPhoto OwnerPhoto { get; set; }

        [DataMember(Name = "tags")]
        public List<string> Tags { get; set; }
    }
}