using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class JobEstimatorProductStyleModel
    {
        public int ProductStyleId { get; set; }
        public int? JobEstimatorClientId { get; set; }
        public int? ProductTypeId { get; set; }
        public string StyleName { get; set; }
        public string StyleImage { get; set; }
        public decimal? MaterialCost { get; set; }
        public decimal? LabourCost { get; set; }
        public decimal? GroundCostFlat { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? GroundCostExcavate { get; set; }
        public decimal? GroundCost { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public IFormFile Image { get; set; }
        public string ProductType { get; set; }
        public bool? IsRollsCalculation { get; set; }
        public string RollSize { get; set; }
    }
}
