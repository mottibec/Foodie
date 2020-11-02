using Newtonsoft.Json;
using System;

namespace Foodie.Core.Api
{
    public class FDAApiResponse<TResponse>
    {
        [JsonProperty("meta")]
        public FDAApiResponseMeta Meta { get; set; }

        [JsonProperty("results")]
        public TResponse[] Results { get; set; }
    }

    public class FDAApiResponseMeta 
    {
        [JsonProperty("disclaimer")]
        public string Disclaimer  { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("terms")]
        public string Terms { get; set; }

        [JsonProperty("last_updated")]
        public  DateTime LastUpdated { get; set; }
    }
}
