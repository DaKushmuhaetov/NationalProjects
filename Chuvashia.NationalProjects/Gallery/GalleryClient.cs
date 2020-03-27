using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chuvashia.NationalProjects.Gallery
{
    public sealed class GalleryClient
    {
        private HttpClient _client;
        public GalleryClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> PostPhoto()
        {

        }
    }
}
