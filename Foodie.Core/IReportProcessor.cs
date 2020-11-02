using Foodie.Core.Api;
using System;
using System.Threading.Tasks;

namespace Foodie.Core
{
    public interface IReportProcessor
    {
        Task<RecallReport[]> GetReportsByDate(DateTime date);

        Task<DateTime> GetReportDate(DateRange dateRange);

        string GetCommonReasonForRecall(RecallReport[] recallReports);
    }
}
