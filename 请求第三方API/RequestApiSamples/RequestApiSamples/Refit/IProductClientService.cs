using Refit;
using RequestApiSamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestApiSamples.Refit
{
    public interface IProductClientService
    {
        [Get("/api/products")]
        Task<IEnumerable<Product>> GetProducts();

        [Get("/api/products/{productId}")]
        Task<Product> GetProduct(int productId);

        [Post("/api/Products")]
        Task CreateProduct(Product product);
    }
}
