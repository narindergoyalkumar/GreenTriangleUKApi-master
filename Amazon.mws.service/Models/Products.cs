using System;
using System.Collections.Generic;
using System.Text;

namespace Amazon.mws.service.Models
{
    public class Products
    {
        //public DateTime? SnapshotDate { get; set; }
        //public string Sku { get; set; }
        //public string Fnsku { get; set; }
        public string Asin { get; set; }
        public string ProductName { get; set; }
        public string Condition { get; set; }
        public int? SalesRank { get; set; }
        public string ProductGroup { get; set; }
        public int? TotalQuantity { get; set; }
        public int? SellableQuanity { get; set; }
        public int? UnsellableQuantity { get; set; }
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
        //public int? NumAfnNewSellers { get; set; }
        //public int? NumAfnUsedSellers { get; set; }
        public string Currency { get; set; }
        public decimal? YourPrice { get; set; }
        public decimal? SalesPrice { get; set; }
        //public decimal? LowestAfnNewPrice { get; set; }
        //public decimal? LowestAfnUsedPrice { get; set; }
        //public decimal? LowestMfnNewPrice { get; set; }
        //public decimal? LowestMfnUsedPrice { get; set; }
        //public int? QuantityToBeChangedLtsf12Mo { get; set; }
        //public int? QuantityInLongTermStorageProgram { get; set; }
        //public int? QuantityWithRemovalsInProgress { get; set; }
        //public int? ProjectedLtsf12Mo { get; set; }
        //public double? PerUnitVolume { get; set; }
        //public string IsHazmat { get; set; }
        //public int? InBoundQuanity { get; set; }
        //public int? AsnLimit { get; set; }
        //public int? InBoundRecommendedQuantity { get; set; }
        //public int? QuantitytoBeChargedLtsf6Mo { get; set; }
        //public int? ProjectedLtsf6Mo { get; set; }
    }
}
