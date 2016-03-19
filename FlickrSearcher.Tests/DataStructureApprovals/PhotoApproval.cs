using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Writers;
using FlickrSearcher.Search;
using FlickrSearcher.Search.Models;
using FlickrSearcher.Tests.Data;
using Newtonsoft.Json;
using Xunit;

namespace FlickrSearcher.Tests.DataStructureApprovals
{
    [UseReporter(typeof (DiffReporter))]
    public class PhotoApproval
    {
        [Fact]
        public void approval()
        {
            var photo = new Photo
            {
                Id = 42,
                ImageUrl = @"http://myurl/api/image/42/small",
                LargeImageUrl = @"http://myurl/api/image/42/large",
                Title = "The test"
            };

            var json = JsonConvert.SerializeObject(
                photo, Formatting.Indented);

            var writer = new ConfigurableTempTextFileWriter(
               Consts.ApprovalsFolder + @"\photo_approval.json",
               json);

            Approvals.Verify(writer);
        }
    }
}
