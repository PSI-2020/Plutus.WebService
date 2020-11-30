using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly HistoryService _historyService = new HistoryService();

        [HttpGet]
        public ActionResult<List<All>> Get()
        {
            return _historyService.LoadDataGrid(0);
        }

        [HttpGet("{index}")]
        public ActionResult<List<All>> Get(int index) =>
            _historyService.LoadDataGrid(index);

        //[HttpPost]
        //public ActionResult<Payment> Post(Payment payment)
        //{
        //    return payment;
        //}
    }
}