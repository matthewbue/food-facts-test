using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FitnessFoods.Application.Interfaces;
using FitnessFoods.Domain.Entities;

namespace FitnessFoods.Application.Services
{
    public class ProductImportService : IProductImportService
    {
        private readonly HttpClient _httpClient;
        private readonly IProductRepository _repository;

        public ProductImportService(HttpClient httpClient, IProductRepository repository)
        {
            _httpClient = httpClient;
            _repository = repository;
        }

        public async Task<List<string>> GetFileNamesAsync()
        {
            var indexUrl = "https://challenges.coode.sh/food/data/json/index.txt";
            var indexTxt = await _httpClient.GetStringAsync(indexUrl);

            return indexTxt.Split('\n')
                           .Where(x => !string.IsNullOrWhiteSpace(x))
                           .ToList();
        }

        public async Task ImportFromUrlAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Erro ao acessar a URL: {url}. Status: {response.StatusCode}");
                }

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var decompressedStream = new GZipStream(stream, CompressionMode.Decompress)) // Descompacta o arquivo
                using (var reader = new StreamReader(decompressedStream))
                {
                    var productsBatch = new List<Product>();

                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        try
                        {
                            using (JsonDocument doc = JsonDocument.Parse(line))
                            {
                                var product = new Product
                                {
                                    Code = doc.RootElement.GetProperty("code").GetString()?.Replace("\"", string.Empty),

                                    Url = doc.RootElement.GetProperty("url").GetString(),
                                    Creator = doc.RootElement.GetProperty("creator").GetString(),
                                    ProductName = doc.RootElement.GetProperty("product_name").GetString(),
                                    Quantity = doc.RootElement.GetProperty("quantity").GetString(),
                                    Brands = doc.RootElement.GetProperty("brands").GetString(),
                                    Categories = doc.RootElement.GetProperty("categories").GetString(),
                                    Labels = doc.RootElement.GetProperty("labels").GetString(),
                                    Cities = doc.RootElement.GetProperty("cities").GetString(),
                                    PurchasePlaces = doc.RootElement.GetProperty("purchase_places").GetString(),
                                    Stores = doc.RootElement.GetProperty("stores").GetString(),
                                    IngredientsText = doc.RootElement.GetProperty("ingredients_text").GetString(),
                                    Traces = doc.RootElement.GetProperty("traces").GetString(),
                                    ServingSize = doc.RootElement.GetProperty("serving_size").GetString(),
                                };

                                productsBatch.Add(product);

                                if (productsBatch.Count >= 100)
                                {
                                    await _repository.AddManyAsync(productsBatch);
                                    productsBatch.Clear();
                                }
                            }
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine($"Erro de deserialização: {ex.Message}");
                        }
                    }
                    if (productsBatch.Any())
                    {
                        await _repository.AddManyAsync(productsBatch);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao importar dados de {url}: {ex.Message}");
            }
        }

        public async Task ImportProductsFromIndexAsync()
        {
            var fileNames = await GetFileNamesAsync();
            var baseUrl = "https://challenges.coode.sh/food/data/json/";

            foreach (var fileName in fileNames)
            {
                var url = $"{baseUrl}{fileName}";
                await ImportFromUrlAsync(url);
            }
        }
    }
}
