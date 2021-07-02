using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Common
{
    public static class Constants
    {
        public static readonly Dictionary<string, int> DeliveryStatus = new Dictionary<string, int>
        {
            {"a",1 },
            {"b",2 },
            {"d",3 },
            {"e",4 },
            {"f",5 },
            {"j",6 },
            {"q",7 },
            {"r",8 },
            {"s",9 },
            {"u",10 }
        };
        public static string SignatureReviewEmailContent = "<p>Hi {0},</p><br />{1} sent you a document to sign.<br/><br/><a style='border:2px solid green;background:transparent;text-decoration:none;color:green;padding:5px;font-weight:bold' href={2}>Review Document</a><br /><br />Many Thanks,<br /><br /> {3}";
    }
}
