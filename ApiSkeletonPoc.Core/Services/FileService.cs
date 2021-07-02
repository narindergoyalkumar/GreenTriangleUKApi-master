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
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        public FileService(IHostingEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public async Task<FileModel> Upload(IFormFile file)
        {
            if (file == null)
                return null;
            var extension = "." + file.FileName.Split('.')[^1];
            string fileName = Guid.NewGuid().ToString() + extension;
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
            return new FileModel { Extension = extension, UniqueName = fileName, OriginalName = file.FileName, Path = filePath, Size = file.Length, UploadedLink = _config.GetValue<string>("BaseUrl") + "Documents/" + fileName };
        }
    }

    public class FileModel
    {
        public string UniqueName { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public string UploadedLink { get; set; }
        public string OriginalName { get; set; }
    }
}
