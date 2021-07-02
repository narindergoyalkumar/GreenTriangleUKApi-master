using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IDocumentService
    {
        Task<Tuple<string, Guid>> UploadDoc(IFormFile document);
    }
}
