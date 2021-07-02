using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class BaseResponseModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public object Response { get; set; }
    }
}
