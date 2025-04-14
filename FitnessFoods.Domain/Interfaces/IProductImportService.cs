namespace FitnessFoods.Application;

public interface IProductImportService
{
    Task<List<string>> GetFileNamesAsync();
    Task ImportFromUrlAsync(string url);
    Task ImportProductsFromIndexAsync();
}