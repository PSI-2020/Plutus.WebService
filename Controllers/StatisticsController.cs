using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IFileManagerRepository _fileManager;
        private readonly IStatisticsRepository _statisticsRepository;
        public StatisticsController(IFileManagerRepository fileManagerRepository, IStatisticsRepository statisticsRepository)
        {
            _fileManager = fileManagerRepository;
            _statisticsRepository = statisticsRepository;
        }

        [HttpGet]
        public ActionResult<string> Get() => _statisticsRepository.GenerateExpenseStatistics(_fileManager) + "\r\n" + _statisticsRepository.GenerateIncomeStatistics(_fileManager);

    }
}