using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FlickrSearcher.Search.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlickrSearcher.Search.Repoitories
{
    public interface IPhotoRepository
    {
        IList<FlickerPhoto> Find(string text, int page);
        FlickerPhotoDetails LoadPhotoDetails(long photoId);
    }

    public class PhotoRepository: IPhotoRepository
    {
        public IList<FlickerPhoto> Find(
            string text, 
            int page)
        {
            var url = string.Format(
                "https://api.flickr.com/services/rest/?" +
                "&method=flickr.photos.search" +
                "&api_key=0750e5b8e98b415cbc0bd5361da74f6a" +
                "&format=json&nojsoncallback=1" +
                "&text={0}" +
                "&per_page=10" +
                "&page={1}",
                text, page);

            var json = MakeGetRequest(url);

            var photos = (JObject.Parse(json)["photos"]["photo"] as JArray);
            
            return JsonConvert
                .DeserializeObject<List<FlickerPhoto>>(photos.ToString());
        }

        public FlickerPhotoDetails LoadPhotoDetails(
            long photoId)
        {
            var url =
                "https://api.flickr.com/services/rest/?" +
                "&method=flickr.photos.getInfo" +
                "&api_key=0750e5b8e98b415cbc0bd5361da74f6a" +
                "&format=json" +
                "&nojsoncallback=1" +
                "&photo_id=" + photoId;

            var json = MakeGetRequest(url);

            return DeserializePhotoDetails(json);
        }
        
        private string MakeGetRequest(string url)
        {
            var httpClient = new HttpClient();

            var responseJson = "";

            Task makeRequest = httpClient.GetAsync(url)
                .ContinueWith(task =>
                {
                    if (task.Result.IsSuccessStatusCode)
                    {
                        task.Result.Content.ReadAsStringAsync()
                            .ContinueWith(t => { responseJson = t.Result; })
                            .Wait();
                    }
                });
            makeRequest.Wait();

            return responseJson;
        }

        private FlickerPhotoDetails DeserializePhotoDetails(string json)
        {
            var photo = JObject.Parse(json)["photo"];

            var tags = photo["tags"]["tag"]
                .Select(t => t["_content"].ToString())
                .ToList();

            var ownerPhoto = new FlickerPhoto
            {
                Id = (string)photo["owner"]["nsid"],
                Server = (int)photo["owner"]["iconserver"],
                Farm = (int)photo["owner"]["iconfarm"],
            };

            return new FlickerPhotoDetails
            {
                PhotoId = (long)photo["id"],
                OwnerUserName = (string)photo["owner"]["username"],
                OwnerRealName = (string)photo["owner"]["realname"],
                OwnerLocation = (string)photo["owner"]["location"],
                Title = (string)photo["title"]["_content"],
                TakenDate = (DateTime?)photo["dates"]["taken"],
                OwnerPhoto = ownerPhoto,
                Tags = tags
            };
        }
    }
}