using Foodie.Core;
using Foodie.Core.Api;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Foodie.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var apiHandler = GetApiHandler();
            var rp = GetReportProcessor(apiHandler);
            var date = await rp.GetReportDate(DateRange.ByYear(2012));
            System.Console.WriteLine($"report_date: {date}");
            var reports = await rp.GetReportsByDate(date);
            var repostsSerialized = JsonConvert.SerializeObject(reports);
            System.Console.WriteLine($"reports: {repostsSerialized}");
            var mostCommonReason = rp.GetCommonReasonForRecall(reports);
            System.Console.WriteLine($"mostCommonReason: {mostCommonReason}");
        }

        public static IFDAApiHandler GetApiHandler()
        {
            var apiSettings = new FDAApiSettings
            {
                ApiAuthType = "Basic",
                BaseUrl = "https://api.fda.gov/food/enforcement.json",
                ApiKey = "WSEEYzxzcapfDzeqLkBVTqsSfUo2vRJzM3wwD4ql",
                DefaultResultLimit = 1000,
            };

            return new FDAApiHandler(apiSettings);
        }

        public static IReportProcessor GetReportProcessor(IFDAApiHandler apiHandler)
        {
            return new ReportProcessor(apiHandler);
        }
    }
}
