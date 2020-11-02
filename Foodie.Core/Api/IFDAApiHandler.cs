using System;
using System.Threading.Tasks;

namespace Foodie.Core.Api
{
    public interface IFDAApiHandler
    {
        Task<FDAApiResponse<RecallReport>> GetReports(DateRange dateRange);

        Task<FDAApiResponse<RecallReport>> GetReports(DateTime date);

        Task<FDAApiResponse<CountReport>> GetReportCount(DateRange dateRange);
    }
}
