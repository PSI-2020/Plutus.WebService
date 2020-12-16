using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;
        public HistoryController(IHistoryService historyService) => _historyService = historyService;

        [HttpGet]
        public List<HistoryElement> Get() => _historyService.LoadDataGrid(0);

        [HttpGet("{index}")]
        public List<HistoryElement> Get(int index) => _historyService.LoadDataGrid(index);

    }
}