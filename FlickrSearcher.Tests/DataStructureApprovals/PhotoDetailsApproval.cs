using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Writers;
using FlickrSearcher.Search.Models;
using FlickrSearcher.Tests.Data;
using Newtonsoft.Json;
using Xunit;

namespace FlickrSearcher.Tests.DataStructureApprovals
{
    [UseReporter(typeof(DiffReporter))]
    public class PhotoDetailsApproval
    {
        [Fact]
        public void approval()
        {
            var photo = new PhotoDetails
            {
                Id = 42,
                IconUrl = @"http://myurl/api/image/42/icon",
                OwnerUserName = "igoryosha",
                OwnerRealName = "Igor Nikolaev",
                OwnerLocation = "Taxi-taxi",
                Title = "The test",
                TakenDate = new DateTime(2012, 12, 21),
                Tags = new List<string> { "vipem","za lubov"}
            };

            var json = JsonConvert.SerializeObject(
                photo, Formatting.Indented);

            var writer = new ConfigurableTempTextFileWriter(
               Consts.ApprovalsFolder + @"\photo_details_approved.json",
               json);

            Approvals.Verify(writer);
        }
    }
}
