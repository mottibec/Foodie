using Foodie.Core.Api;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Core.Api
{
    public class FDAApiHandler : IFDAApiHandler
    {
        private FDAApiSettings _apiSettings;
        private HttpClient _httpClient;
        private ILogger _logger;

        public FDAApiHandler(FDAApiSettings apiSettings, ILogger logger = null)
        {
            _logger = logger;
            _apiSettings = apiSettings;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_apiSettings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_apiSettings.ApiAuthType, _apiSettings.ApiKey);
        }

        private async Task<FDAApiResponse<T>> ExecuteRequest<T>(string url)
        {
            _logger?.LogInformation($"ExecuteRequest for {url}");
            var reportResponseMessage = await _httpClient.GetAsync($"{url}&limit={_apiSettings.DefaultResultLimit}");
            if (!reportResponseMessage.IsSuccessStatusCode)
            {
                _logger?.LogError($"ExecuteRequest Failed With Status Code {reportResponseMessage.StatusCode}");
                throw new Exception("Unsuccessful Request exception");
            }

            using var content = await reportResponseMessage.Content.ReadAsStreamAsync();
            var serializer = new JsonSerializer();
            using var sr = new StreamReader(content);
            using var jsonTextReader = new JsonTextReader(sr);
            var fdaResponse = serializer.Deserialize<FDAApiResponse<T>>(jsonTextReader);
            return fdaResponse;
        }

        public Task<FDAApiResponse<RecallReport>> GetReports(DateRange dateRange)
        {
            //date format yyyymmdd
            var requestUrl = $"?search=report_date:[{ToApiFormat(dateRange.Start)}+TO+{ToApiFormat(dateRange.End)}]";
            return ExecuteRequest<RecallReport>(requestUrl);
        }

        public Task<FDAApiResponse<RecallReport>> GetReports(DateTime date)
        {
            //date format yyyymmdd
            var requestUrl = $"?search=report_date:{ToApiFormat(date)}";
            return ExecuteRequest<RecallReport>(requestUrl);
        }

        public Task<FDAApiResponse<CountReport>> GetReportCount(DateRange dateRange)
        {
            var requestUrl = $"?search=report_date:[{ToApiFormat(dateRange.Start)}+TO+{ToApiFormat(dateRange.End)}]&count=report_date";
            return ExecuteRequest<CountReport>(requestUrl);
        }

        private string ToApiFormat(DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
