using FitnessFoods.Application.Interfaces;
using FitnessFoods.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FitnessFoods.Infrastructure.Repositories
{
    public class ImportHistoryRepository : IImportHistoryRepository
    {
        private readonly IMongoCollection<ImportHistory> _collection;
        public ImportHistoryRepository(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var db = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _collection = db.GetCollection<ImportHistory>("ImportHistories");
        }

        public async Task SaveAsync(ImportHistory history) =>
            await _collection.InsertOneAsync(history);

        public async Task<ImportHistory?> GetLastAsync() =>
            await _collection.Find(_ => true).SortByDescending(x => x.RunAt).FirstOrDefaultAsync();

    }

}
