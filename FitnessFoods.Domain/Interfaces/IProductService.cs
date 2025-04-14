using FitnessFoods.Domain.Entities;

namespace FitnessFoods.Domain.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> ListAllAsync(int page, int pageSize);
        Task<Product?> GetByCodeAsync(string code);
        Task UpdateAsync(string code, Product product);
        Task DeleteAsync(string code);

    }
}