using ApiSkeletonPoc.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApiSkeletonPoc.Core.Common.Enums;

namespace ApiSkeletonPoc.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        public ImageService(IHostingEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }
        public async Task<string> UploadImage(IFormFile file)
        {
            if (file == null)
                return null;
            if (CheckIfImageFile(file))
            {
                var imageName = await WriteFile(file).ConfigureAwait(false);
                return _config.GetValue<string>("BaseUrl") + "Images/" + imageName;
            }

            return "Invalid image file";
        }
        public async Task<string> UploadImage(IFormFile file, string asin)  // uploading product image
        {
            if (file == null)
                return null;
            var imageName = await UploadProductImage(file, asin).ConfigureAwait(false);
            return _config.GetValue<string>("BaseUrl") + "Images/" + imageName;
        }

        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return GetImageFormat(fileBytes) != ImageFormat.unknown;
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            if (file == null)
                return null;
            var extension = "." + file.FileName.Split('.')[^1];
            string fileName = Guid.NewGuid().ToString() + extension;
            if (!Directory.Exists(Path.Combine(_env.WebRootPath, "Images")))
            {
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "Images"));
            }
            string directoryPath = Path.Combine(_env.WebRootPath, "Images");
            var filePath = Path.Combine(directoryPath, fileName);
            using (var bits = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(bits).ConfigureAwait(false);
            }
            return fileName;
        }

        private async Task<string> UploadProductImage(IFormFile file, string asin)
        {
            if (file == null)
                return null;
            var extension = "." + file.FileName.Split('.')[^1];
            string fileName = asin + extension;
            if (!Directory.Exists(Path.Combine(_env.WebRootPath, "Images")))
            {
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "Images"));
            }
            if (File.Exists(Path.Combine(_env.WebRootPath, $"Images/{fileName}")))
            {
                File.Delete(Path.Combine(_env.WebRootPath, $"Images/{fileName}"));
            }
            string directoryPath = Path.Combine(_env.WebRootPath, "Images");
            var filePath = Path.Combine(directoryPath, fileName);
            using (var bits = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(bits).ConfigureAwait(false);
            }
            return fileName + "?" + DateTime.Now.ToString("HH:mm:ss");
        }
        private ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };              // PNG
            var tiff = new byte[] { 73, 73, 42 };                  // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };                 // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;
        }
    }
}
