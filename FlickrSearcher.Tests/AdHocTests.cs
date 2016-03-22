using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace FlickrSearcher.Tests
{
    public class AdHocTests
    {
        //[Fact]
        public async Task check_flckr()
        {
            var client = new HttpClient();

            var url =
                "https://api.flickr.com/services/rest/?" +
                "&method=flickr.photos.search" +
                "&api_key=0750e5b8e98b415cbc0bd5361da74f6a" +
                "&per_page=10" +
                "&page=2" +
                "&format=json&nojsoncallback=1" +
                "&text=red";

            var response = await client.GetAsync(url);

            var text = await response.Content.ReadAsStringAsync();

            response
                .IsSuccessStatusCode
                .Should()
                .BeTrue();
        }

        //[Fact]
        public async Task request_photo()
        {
            //{"id":"25750968675","owner":"125349441@N03","secret":"5c4b5e441a","server":"1460","farm":2,"title":"Red Tail","ispublic":1,"isfriend":0,"isfamily":0}
            
            var client = new HttpClient();

            //farm - id: 1
            //server - id: 2
            //photo - id: 1418878
            //secret: 1e92283336
            //size: m

            var url = "https://farm2.staticflickr.com" +
                      "/1460" +
                      "/25750968675_5c4b5e441a_m.jpg";
            
            var response = await client.GetAsync(url);

            var content = await response.Content.ReadAsByteArrayAsync();

            content.Length
                .Should()
                .Be(25254);
        }

        //[Fact]
        public async Task get_image_details()
        {
            var client = new HttpClient();

            var url =
                "https://api.flickr.com/services/rest/?" +
                "&method=flickr.photos.getInfo" +
                "&api_key=0750e5b8e98b415cbc0bd5361da74f6a" +
                "&format=json" +
                "&nojsoncallback=1" +
                "&photo_id=25750968675";

            var response = await client.GetAsync(url);

            var text = await response.Content.ReadAsStringAsync();

            response
                .IsSuccessStatusCode
                .Should()
                .BeTrue();
        }
    }
    
}
