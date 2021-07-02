using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class JobEstimatorProductStyle
    {
        public int ProductStyleId { get; set; }
        public int? JobEstimatorClientId { get; set; }
        public int? ProductTypeId { get; set; }
        public string StyleName { get; set; }
        public string StyleImage { get; set; }
        public decimal? MaterialCost { get; set; }
        public decimal? LabourCost { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? GroundCostFlat { get; set; }
        public decimal? GroundCostExcavate { get; set; }
        public decimal? GroundCost { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsRollsCalculation { get; set; }
        public string RollSize { get; set; }

        [JsonIgnore]
        public virtual JobEstimatorClient JobEstimatorClient { get; set; }
        //[JsonIgnore]
        public virtual JobEstimatorProductType ProductType { get; set; }
    }
}
