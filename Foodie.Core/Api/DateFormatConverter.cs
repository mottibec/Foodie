﻿using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foodie.Core.Api
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
