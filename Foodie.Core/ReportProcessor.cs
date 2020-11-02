using Foodie.Core.Api;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Core
{
    public class ReportProcessor : IReportProcessor
    {
        private readonly IFDAApiHandler _apiHandler;
        private readonly ILogger _logger;

        public ReportProcessor(IFDAApiHandler apiHandler, ILogger logger = null)
        {
            _apiHandler = apiHandler;
            _logger = logger;
        }

        public async Task<RecallReport[]> GetReportsByDate(DateTime date)
        {
            _logger?.LogInformation($"Getting Reports for {date}");
            var reports = await _apiHandler.GetReports(date);
            _logger?.LogInformation($"Successfully retrieved {reports.Results.Length} reports for {date}");
            return reports.Results;
        }

        public async Task<DateTime> GetReportDate(DateRange dateRange)
        {
            var reports = await _apiHandler.GetReportCount(dateRange);
            var minCount = int.MaxValue;
            var minCountDate = DateTime.MinValue;
            foreach (var datedReport in reports.Results)
            {
                if (datedReport.Count < minCount)
                {
                    minCount = datedReport.Count;
                    minCountDate = datedReport.Date;
                }
            }

            return minCountDate;
        }

        public string GetCommonReasonForRecall(RecallReport[] recallReports)
        {
            //split all strings to words by spaces and join them to one IEnumerable<string>
            var recallReasonWords = recallReports.SelectMany(report => report.ReasonForRecall.Split(' '));
            var wordDictionary = new Dictionary<string, int>();

            //iterate over all words longer then 4, if the are in the word Dictionary increase the count if not add them
            foreach (var recallReasonWord in recallReasonWords)
            {
                if (recallReasonWord.Length < 4)
                {
                    continue;
                }
                if (wordDictionary.ContainsKey(recallReasonWord))
                {
                    wordDictionary[recallReasonWord] = ++wordDictionary[recallReasonWord];
                }
                else
                {
                    wordDictionary.Add(recallReasonWord, 1);
                }
            }

            //iterate over the wordDictionary to find the word with the highest occurrence
            var maxOccurrence = 0;
            var maxOccurrenceWord = string.Empty;
            foreach (var word in wordDictionary)
            {
                if (word.Value > maxOccurrence)
                {
                    maxOccurrence = word.Value;
                    maxOccurrenceWord = word.Key;
                }
            }
            return maxOccurrenceWord;
        }
    }
}
