using Microsoft.AspNetCore.Mvc;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly FileManager _fileManager = new FileManager();
        private readonly StatisticsService _statisticsService = new StatisticsService();

        [HttpGet]
        public ActionResult<string> Get() => _statisticsService.GenerateExpenseStatistics(_fileManager) + "\r\n" + _statisticsService.GenerateIncomeStatistics(_fileManager);

    }
}