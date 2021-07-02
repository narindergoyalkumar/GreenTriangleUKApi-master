using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class ValveModel
    {
        public Guid Id { get; set; }
        public string ValveId { get; set; }
        [Required(ErrorMessage = "QR is required")]
        public string Qrid { get; set; }
        [Required(ErrorMessage = "DMA Name is required")]
        public string DmaName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string AssetId { get; set; }
        public string BvId { get; set; }
        public string ValveSize { get; set; }
        public string Direction { get; set; }
        public string Comment { get; set; }
        public string BvcontrolNumber { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifedDate { get; set; }
        public List<ValveEventModel> Events { get; set; }
    }
}
