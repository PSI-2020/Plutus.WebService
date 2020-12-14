using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsRepository;
        public StatisticsController(IStatisticsService statisticsRepository) => _statisticsRepository = statisticsRepository;

        [HttpGet]
        public string Get() => _statisticsRepository.GenerateExpenseStatistics() + "\r\n" + _statisticsRepository.GenerateIncomeStatistics();

    }
}