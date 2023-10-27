using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CEApp
{
    public abstract class HttpClientCE
    {
        private readonly Uri _baseUri;
        protected HttpClientCE()
        {
            _baseUri = new Uri("https://localhost:7206/");
        }
        protected HttpClient GetHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = _baseUri
            };
            return client;
        }
    }
}

