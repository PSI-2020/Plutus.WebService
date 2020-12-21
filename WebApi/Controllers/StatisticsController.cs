using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService) => _statisticsService = statisticsService;

        [HttpGet]
        public string Get() => _statisticsService.GenerateExpenseStatistics() + "\r\n" + _statisticsService.GenerateIncomeStatistics();

        [HttpGet("{type}/{category}")]
        public decimal Get(DataType type, string category) => _statisticsService.CategorySum(category, type);

    }
}