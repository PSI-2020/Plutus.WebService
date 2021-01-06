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
        public List<HistoryElement> Get() => _historyService.LoadDataGrid(0, 0, int.MaxValue, new Filters());

        [HttpGet("{index}/{page}/{perpage}/{used}/{Name}/{expFlag}/{incFlag}/{amountFilter}/{amfrom}/{amto}/{dFilter}/{dateF}/{dateto}")]
        public List<HistoryElement> Get(int index, int page, int perpage, bool used, string name, int expFlag, int incFlag, int amountFilter, int amFrom, int amTo, bool dFilter, int dateF, int dateto)
        {
            var filter = new Filters
            {
                Used = used,
                NameFiter = name,
                ExpFlag = expFlag,
                IncFlag = incFlag,
                AmountFilter = amountFilter,
                AmountFrom = amFrom,
                AmountTo = amTo,
                DateFilter = dFilter,
                DateFrom = dateF,
                DateTo = dateto
            };
            return _historyService.LoadDataGrid(index, page, perpage, filter);
        }


        [HttpGet("{index}/{perpage}/{used}/{Name}/{expFlag}/{incFlag}/{amountFilter}/{amfrom}/{amto}/{dFilter}/{dateF}/{dateto}")]
        public int Get(int index, int perpage, bool used, string name, int expFlag, int incFlag, int amountFilter, int amFrom, int amTo, bool dFilter, int dateF, int dateto)
        {
            var filter = new Filters
            {
                Used = used,
                NameFiter = name,
                ExpFlag = expFlag,
                IncFlag = incFlag,
                AmountFilter = amountFilter,
                AmountFrom = amFrom,
                AmountTo = amTo,
                DateFilter = dFilter,
                DateFrom = dateF,
                DateTo = dateto
            };
            return _historyService.GivePageCount(index, perpage, filter);
        }

    }
}