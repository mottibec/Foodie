using Foodie.Core;
using Foodie.Core.Api;
using Moq;
using NUnit.Framework;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Foodie.UnitTest
{
    public class Tests
    {
        [Test]
        public async Task Test_ReportProcessor_Min_Report_Count()
        {
            //arrange
            var moqApiHandler = new Mock<IFDAApiHandler>();

            //Create fake API data
            var minReportDateTime = new DateTime(2012, 07, 25);
            var fakeData = new[]
            {
                new CountReport
                {
                    Count = 100,
                    Date = new DateTime(2012,06,20)
                },
                new CountReport
                {
                    Count = 1000,
                    Date = new DateTime(2012,07,11)
                },
                new CountReport
                {
                    Count = 10,
                    Date = minReportDateTime
                },
            };
            var fakeFDAApiResponse = new FDAApiResponse<CountReport>
            {
                Results = fakeData
            };

            //mock GetReportCount response to custom result
            moqApiHandler.Setup(apiHandler => apiHandler.GetReportCount(It.IsAny<DateRange>()))
                .Returns(Task.FromResult(fakeFDAApiResponse));
            var mockedApiHandler = moqApiHandler.Object;
            var reportProcessor = new ReportProcessor(mockedApiHandler);

            //act 
            var minDate = await reportProcessor.GetReportDate(DateRange.ByYear(2012));

            //assert
            Assert.That(minDate, Is.EqualTo(minReportDateTime));
        }

        [Test]
        public void Test_ReportProcessor_Most_Common_Recall_Reason()
        {
            var moqApiHandler = new Mock<IFDAApiHandler>();
            var reportProcessor = new ReportProcessor(moqApiHandler.Object);
            var fakeReports = new[]
            {
                new RecallReport
                {
                    ReasonForRecall = "the product was contaminated"
                },
                new RecallReport
                {
                    ReasonForRecall = "Mostly contaminated items some expired"
                }
                ,
                new RecallReport
                {
                    ReasonForRecall = "burend and contaminated items"
                },
                new RecallReport
                {
                    ReasonForRecall = "item in delivery was contaminated"
                }
            };
            var result = reportProcessor.GetCommonReasonForRecall(fakeReports);
            var expected = "contaminated";
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}