using SimpleShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Infrastructure.Services
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
