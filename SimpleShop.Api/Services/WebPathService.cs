using SimpleShop.Application.Common.Interfaces;

namespace SimpleShop.Api.Services
{
    public class WebPathService : IPathService
    {
        private readonly IWebHostEnvironment _env;
        public WebPathService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string GetWebRootPath()
        {
            return _env.WebRootPath;
        }
    }
}
