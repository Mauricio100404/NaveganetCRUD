using MongoDB.Bson;
using MongoDBAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MongoDBAPI.UI.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteProduct(string id)
        {
            await _httpClient.DeleteAsync($"api/product/{id}");
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Product>>
                (await _httpClient.GetStreamAsync($"api/product"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Product> GetProductDetails(string id)
        {
            return await JsonSerializer.DeserializeAsync<Product>
                (await _httpClient.GetStreamAsync($"api/product/{id}"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task SaveProduct(Product product)
        {
            try
            {


                var productJson = new StringContent(JsonSerializer.Serialize(product),
                    Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                if (string.IsNullOrEmpty(product.Id))
                {
                    product.Id = ObjectId.GenerateNewId().ToString();
                    productJson = new StringContent(JsonSerializer.Serialize(product),
                        Encoding.UTF8, "application/json");

                    response = await _httpClient.PostAsync("api/product", productJson);
                }
                else
                {
                    response = await _httpClient.PutAsync($"api/product/{product.Id}", productJson);
                }

                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                // Manejar el error
                var errorMessage = ex.Message; // Aquí puedes loguear el error o mostrarlo
            }
        }
    }
}
