using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Writers;
using FlickrSearcher.Search.Repoitories;
using FlickrSearcher.Tests.Data;
using Newtonsoft.Json;
using Xunit;

namespace FlickrSearcher.Tests.PhotoRepositoryTests
{
    [UseReporter(typeof(DiffReporter))]
    public class LoadPhotoDetails
    {
        [Fact]
        public void approval()
        {
            // arrange
            var sut = new PhotoRepository();

            // act
            var actual = sut.LoadPhotoDetails(25224434769);

            // assert
            var json = JsonConvert.SerializeObject(actual, Formatting.Indented);

            var writer = new ConfigurableTempTextFileWriter(
                Consts.ApprovalsFolder + @"\photo_repo_load_details_result_approved.json",
                json);

            Approvals.Verify(writer);
        }
    }
}
