using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class MwsProductsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Asin { get; set; }
        public int? LeadTimeDays { get; set; }
        public int? SellableQuantity { get; set; }
        public int? InvAge0To90Days { get; set; }
        public int? InvAge91to181Days { get; set; }
        public int? InvAge181To270Days { get; set; }
        public int? InvAge271To365Days { get; set; }
        public int? InvAge365PlusDays { get; set; }
        public int? UnitsShippedLast24Hrs { get; set; }
        public int? UnitsShippedLast7Days { get; set; }
        public int? UnitsShippedLast30Days { get; set; }
        public int? UnitsShippedLast90Days { get; set; }
        public int? UnitsShippedLast180Days { get; set; }
        public int? UnitsShippedLast365Days { get; set; }
        public string WeeksOfCoverT7 { get; set; }
        public string WeeksOfCoverT30 { get; set; }
        public string WeeksOfCoverT90 { get; set; }
        public string WeeksOfCoverT180 { get; set; }
        public string WeeksOfCoverT365 { get; set; }
        public string ShortName { get; set; }
        public int? InTransit { get; set; }
        public string Image { get; set; }
        public DateTime OutOfStockDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ReOrderColorCode { get; set; }
        public int ReOrderDays { get; set; }
    }
}
