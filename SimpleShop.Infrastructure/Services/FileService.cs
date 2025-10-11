using SimpleShop.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IPathService _pathService;
        public FileService(IPathService pathService) => _pathService = pathService;

        public async Task<string> SaveFileAsync(IFormFile file, string filePath)
        {
            if (file == null || file.Length == 0)
                return string.Empty;
            var webRootPath = _pathService.GetWebRootPath();
            var uploadPath = Path.Combine(webRootPath, filePath);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(uploadPath, uniqueFileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return uniqueFileName;
        }
        public void DeleteFile(string fileName, string filePath)
        {
            if (string.IsNullOrEmpty(fileName)) return;
            var fullPath = Path.Combine(_pathService.GetWebRootPath(), fileName);
            if(File.Exists(fullPath))
                File.Delete(fullPath);
        }

    }
}
