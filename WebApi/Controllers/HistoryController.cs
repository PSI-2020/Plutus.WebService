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
        public List<HistoryElement> Get() => _historyService.LoadDataGrid(0,0,int.MaxValue);

        [HttpGet("{index}/{page}/{perpage}")]
        public List<HistoryElement> Get(int index, int page, int perpage) => _historyService.LoadDataGrid(index, page, perpage);

        [HttpGet("{index}/{perpage}")]
        public int Get(int index, int perpage) => _historyService.GivePageCount(index, perpage);

    }
}