using System.Threading.Tasks;
using FitnessFoods.Application;
using FitnessFoods.Application.Services;
using Microsoft.AspNetCore.Mvc;
namespace FitnessFoods.API.Controllers;
[ApiController]
[Route("api/import")]
public class ImportController : ControllerBase
{
    private readonly IProductImportService _importService;
    public ImportController(IProductImportService importService)
    {
        _importService = importService;
    }

    [HttpPost]
    public async Task<IActionResult> Import([FromQuery] string url)
    {
        await _importService.ImportFromUrlAsync(url);
        return Ok("Importação concluída!");
    }

}
