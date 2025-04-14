using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessFoods.Application.Interfaces;
using FitnessFoods.Application.Services;
using FitnessFoods.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FitnessFoods.Infrastructure.Services
{

    public class CronJobService : BackgroundService
    {
        private readonly IServiceProvider _provider; private readonly IConfiguration _config;
        public CronJobService(IServiceProvider provider, IConfiguration config)
        {
            _provider = provider;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var delay = TimeSpan.FromMinutes(1);

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _provider.CreateScope();
                var importService = scope.ServiceProvider.GetRequiredService<ProductImportService>();
                var historyRepo = scope.ServiceProvider.GetRequiredService<IImportHistoryRepository>();

                try
                {
                    var indexUrl = "https://challenges.coode.sh/food/data/json/index.txt";
                    var httpClient = new HttpClient();
                    var indexTxt = await httpClient.GetStringAsync(indexUrl);
                    var fileNames = indexTxt.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).Take(1);

                    int count = 0;
                    foreach (var name in fileNames)
                    {
                        string url = $"https://challenges.coode.sh/food/data/json/{name}";
                        await importService.ImportFromUrlAsync(url);
                        count += 100;
                    }

                    await historyRepo.SaveAsync(new ImportHistory
                    {
                        RunAt = DateTime.UtcNow,
                        ImportedCount = count
                    });
                }
                catch
                {
                }

                await Task.Delay(delay, stoppingToken);
            }
        }

    }


}
