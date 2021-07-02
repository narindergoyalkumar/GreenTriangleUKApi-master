using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Extensions
{
    public static class HelperExtensions
    {
        public static string ToMwsDecimalPrecision(this string value)
        {
            if (null == value)
                return null; // or throw exception, or return "0,00" or return "?"
            try
            {
                if (!string.IsNullOrEmpty(value) && value != "Infinite")
                {
                    if (decimal.TryParse(value, out decimal result))
                    {
                        value = result.ToString("0.00");
                    }
                }
                return value;
            }
            catch (FormatException)
            {
                return "0,00"; // or "?"
            }
        }
    }
}
