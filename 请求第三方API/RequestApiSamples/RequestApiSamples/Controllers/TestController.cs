using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RequestApiSamples.ApiServices;
using RequestApiSamples.Models;
using RequestApiSamples.Refit;

namespace RequestApiSamples.Controllers
{
    public class TestController : Controller
    {
        private readonly IHttpClientFactory clientFactory;

        public TestController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = Enumerable.Empty<Product>();

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:56774/api/products");
            request.Headers.Add("userid", "xcode");

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            }

            return View(products);
        }

        public async Task<IActionResult> NameOfHttpClient()
        {
            IEnumerable<Product> products = Enumerable.Empty<Product>();

            var request = new HttpRequestMessage(HttpMethod.Get, "api/products");
            request.Headers.Add("userid", "xcode");

            var client = clientFactory.CreateClient("api1");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            }

            return View("Index", products);
        }

        public async Task<IActionResult> TypeOfHttpClient([FromServices] ProductClientService productClientService)
        {
            IEnumerable<Product> products = null;

            try
            {
                products = await productClientService.GetProducts();
            }
            catch (HttpRequestException)
            {
                products = Array.Empty<Product>();
            }

            return View("Index", products);
        }

        public async Task<IActionResult> TypeOfHttpClient2([FromServices] IProductClientService productClientService)
        {
            IEnumerable<Product> products = null;

            try
            {
                products = await productClientService.GetProducts();
            }
            catch (HttpRequestException)
            {
                products = Array.Empty<Product>();
            }

            return View("Index", products);
        }

        public async Task<IActionResult> TypeOfHttpClient3([FromServices] IProductClientService productClientService, int id)
        {
            Product product = null;

            try
            {
                product = await productClientService.GetProduct(id);
            }
            catch (HttpRequestException)
            {
                product = null;
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromServices] IProductClientService productClientService, Product product)
        {
            await productClientService.CreateProduct(product);
            return RedirectToAction(nameof(Index));
        }
    }
}