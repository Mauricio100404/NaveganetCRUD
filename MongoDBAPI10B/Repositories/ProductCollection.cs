using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBAPI.Model;


namespace MongoDBAPI10B.Repositories
{
    public class ProductCollection:IProductCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Product>Collection;
        public ProductCollection()
        {
            Collection = _repository.db.GetCollection<Product>("Productos");
        }

        public async Task DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(s => s.Id, id);
            await Collection.DeleteOneAsync(filter);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await Collection.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }

        public async Task InsertProduct(Product product)
        {
            await Collection.InsertOneAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            var filter = Builders<Product>
                .Filter
                .Eq(s => s.Id, product.Id);

            await Collection.ReplaceOneAsync(filter, product);
        }
    }
}
