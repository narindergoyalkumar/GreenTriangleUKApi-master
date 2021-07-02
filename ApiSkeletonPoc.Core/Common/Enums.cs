using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Common
{
    public static class Enums
    {
        public enum UserRole
        {
            User = 1,
            AmazonSalesAdmin = 2,
            AmazonSalesUser = 3,
            Admin = 4,
            JobEstimatorAdmin = 5,
            JobEstimatorClient = 6
        }
        public enum Module
        {
            Contacts = 1
        }
        public enum ContactType
        {
            Individual = 1,
            Organisation = 2
        }
        public enum DeliveryStatus
        {
            a = 1,
            b = 2,
            d = 3,
            e = 4,
            f = 5,
            j = 6,
            q = 7,
            r = 8,
            s = 9,
            u = 10
        }

        public enum CustomFieldTypes
        {
            Text = 1,
            Date = 2,
            TickBox = 3,
            List = 4,
            Number = 5,
            Spacer = 6
        }
        public enum ImageFormat
        {
            bmp,
            jpeg,
            gif,
            tiff,
            png,
            unknown
        }
        public enum JobType
        {
            Recurring=1,
            Project=2
        }

    }
}
