﻿using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

using Newtonsoft.Json;
using Intercom.Converters.ClassConverters;

namespace Intercom.Data
{
    [JsonConverter(typeof(CompanyCountJsonConverter))]
    public class CompanySegmentCount : SegmentCount
    {
        public CompanySegmentCount()
        {
        }
    }
}

