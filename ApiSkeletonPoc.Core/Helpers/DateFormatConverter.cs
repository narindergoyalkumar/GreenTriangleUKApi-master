using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Helpers
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter()
        {
            DateTimeFormat = "dd/MM/yyyy";
        }
    }
}
