using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApiSkeletonPoc.Core.Common
{
    public static class Utility
    {
        public static  decimal PullMeanAverage(params decimal[] items)
        {
            return Queryable.Average(items.AsQueryable());
        }
        public static string CreateRandomString(int length=8)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static string CreateRandomAlphabets(int length = 4)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static DateTime GetStartDateOfWeek()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime start = DateTime.Now.AddDays(-days);
            return start;
        }
        public static DateTime GetEndDateOfWeek()
        {
            DateTime end = GetStartDateOfWeek().AddDays(6);
            return end;
        }

    }
}
