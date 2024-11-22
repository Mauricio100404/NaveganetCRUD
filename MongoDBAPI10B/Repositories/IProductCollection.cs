using MongoDBAPI.Model;

namespace MongoDBAPI10B.Repositories
{
    public interface IProductCollection
    {
        Task InsertProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(string id);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(string id);
    }
}
