using Microsoft.AspNetCore.Http;

namespace SimpleShop.Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string filePath);
        void DeleteFile(string fileName, string filePath);
    }
}
