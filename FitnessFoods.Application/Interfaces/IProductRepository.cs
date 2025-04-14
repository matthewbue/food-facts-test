using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessFoods.Domain.Entities;

namespace FitnessFoods.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(int page, int pageSize);
        Task<Product?> GetByCodeAsync(string code); 
        Task AddManyAsync(List<Product> products); 
        Task UpdateAsync(string code, Product product);
        Task SoftDeleteAsync(string code);
    }
}
