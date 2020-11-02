using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foodie.Core.Api
{
    public class CountReport
    {
        [JsonProperty("time"), JsonConverter(typeof(DateFormatConverter), "yyyyMMdd")]
        public DateTime Date { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
