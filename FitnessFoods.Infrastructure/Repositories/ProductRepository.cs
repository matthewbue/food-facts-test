
using FitnessFoods.Application.Interfaces;
using FitnessFoods.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FitnessFoods.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;
        public ProductRepository(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var db = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _collection = db.GetCollection<Product>(config["DatabaseSettings:ProductsCollectionName"]);
        }

        public async Task<List<Product>> GetAllAsync(int page, int pageSize) =>
            await _collection.Find(_ => true).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();

        public async Task<Product?> GetByCodeAsync(string code) =>
            await _collection.Find(p => p.Code == code).FirstOrDefaultAsync();

        public async Task AddManyAsync(List<Product> products) =>
            await _collection.InsertManyAsync(products);

        public async Task UpdateAsync(string code, Product product) =>
            await _collection.ReplaceOneAsync(p => p.Code == code, product);

        public async Task SoftDeleteAsync(string code) =>
            await _collection.UpdateOneAsync(p => p.Code == code,
                Builders<Product>.Update.Set(p => p.Status, ProductStatus.Trash));

    }
}