using MongoDB.Driver;

namespace MongoDBAPI10B.Repositories
{
    public class MongoDBRepository
    {
        public MongoClient client;
        public IMongoDatabase db;
        public MongoDBRepository()
        {
            client = new MongoClient("mongodb+srv://Mauricio:mauricio123@cluster1.fmamf.mongodb.net/?retryWrites=true&w=majority&appName=Cluster1");
            db = client.GetDatabase("Inventario");

        }
    }
}
