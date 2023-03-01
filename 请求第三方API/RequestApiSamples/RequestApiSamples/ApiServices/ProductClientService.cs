using RequestApiSamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestApiSamples.ApiServices
{
    public class ProductClientService
    {
        public HttpClient Client { get; }

        public ProductClientService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("http://localhost:56774/");
            httpClient.DefaultRequestHeaders.Add("appkey", "xcode");
            Client = httpClient;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var response = await Client.GetAsync("api/products");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<IEnumerable<Product>>();

            return result;
        }
    }
}
