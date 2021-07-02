using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImage(IFormFile file);
        Task<string> UploadImage(IFormFile file, string asin);
    }
}
