using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessFoods.Application.Interfaces;
using FitnessFoods.Domain.Entities;
using FitnessFoods.Domain.Interfaces;

namespace FitnessFoods.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> ListAllAsync(int page, int pageSize)
            => await _repository.GetAllAsync(page, pageSize);

        public async Task<Product?> GetByCodeAsync(string code)
            => await _repository.GetByCodeAsync(code);

        public async Task UpdateAsync(string code, Product product)
            => await _repository.UpdateAsync(code, product);

        public async Task DeleteAsync(string code)
            => await _repository.SoftDeleteAsync(code);

    }

}
