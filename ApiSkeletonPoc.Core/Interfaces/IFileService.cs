using ApiSkeletonPoc.Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IFileService
    {
        Task<FileModel> Upload(IFormFile file);
        void Delete(string path);
    }
}
