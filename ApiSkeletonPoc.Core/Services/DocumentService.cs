using ApiSkeletonPoc.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ApiSkeletonPoc.Core.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        public DocumentService(IHostingEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }
        public async Task<Tuple<string, Guid>> UploadDoc(IFormFile document)
        {
            if (document == null)
                return null;
            return await WriteFile(document);
        }
        private async Task<Tuple<string, Guid>> WriteFile(IFormFile file)
        {
            if (file == null)
                return null;
            Guid guid = Guid.NewGuid();
            var extension = "." + file.FileName.Split('.')[^1];
            string fileName = guid.ToString() + extension;
            if (!Directory.Exists(Path.Combine(_env.WebRootPath, "Documents")))
            {
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "Documents"));
            }
            string directoryPath = Path.Combine(_env.WebRootPath, "Documents");
            var filePath = Path.Combine(directoryPath, fileName);
            using (var bits = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(bits).ConfigureAwait(false);
            }
            return new Tuple<string, Guid>(_config.GetValue<string>("BaseUrl") + "Documents/" + fileName, guid);
        }
    }
}
