using System;
using System.Collections.Generic;
using System.Text;

namespace Foodie.Core.Api
{
    public class FDAApiSettings
    {
        //Auth type (Bearer, Basic)
        public string ApiAuthType { get; set; }

        //Api key
        public string ApiKey { get; set; }

        //Base url for the api
        public string BaseUrl { get; set; }

        //Unless otherwise specified, the API will return only one matching record for a search
        public int DefaultResultLimit { get; set; }
    }
}
