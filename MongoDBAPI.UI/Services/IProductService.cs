using MongoDBAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBAPI.UI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductDetails(string id);
        Task SaveProduct (Product product);
        Task DeleteProduct (string id);
    }
}
