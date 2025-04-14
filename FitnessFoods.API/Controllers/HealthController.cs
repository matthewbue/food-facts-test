using System;
using System.Threading.Tasks;
using FitnessFoods.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessFoods.API.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        private readonly IImportHistoryRepository _historyRepo;
        public HealthController(IImportHistoryRepository historyRepo)
        {
            _historyRepo = historyRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var memory = GC.GetTotalMemory(false);
            var uptime = Environment.TickCount64 / 1000;
            var last = await _historyRepo.GetLastAsync();

            return Ok(new
            {
                status = "OK",
                memoryUsageBytes = memory,
                uptimeSeconds = uptime,
                lastCronRunAt = last?.RunAt.ToString("s") ?? "never",
                lastImportCount = last?.ImportedCount ?? 0
            });
        }
    }
}

