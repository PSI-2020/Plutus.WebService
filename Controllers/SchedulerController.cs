using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private readonly FileManager _fileManager = new FileManager();
        private readonly SchedulerService _schedulerService = new SchedulerService();
        private List<ScheduledPayment> ReadExpenses() => _fileManager.ReadFromFile<ScheduledPayment>(DataType.MonthlyExpenses);
        private List<ScheduledPayment> ReadIncomes() => _fileManager.ReadFromFile<ScheduledPayment>(DataType.MonthlyIncome);
        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            var result = "";
            var expenses = ReadExpenses();
            var incomes = ReadIncomes();
            if (expenses.Any())
            {
                for (var x = 0; x < expenses.Count; x++)
                {
                    result = result + _schedulerService.ShowPayment(x, DataType.MonthlyExpenses) + "\r\n" + "\r\n";
                }
            }
            if (incomes.Any())
            {
                for (var x = 0; x < incomes.Count; x++)
                {
                    result = result + _schedulerService.ShowPayment(x, DataType.MonthlyIncome) + "\r\n" + "\r\n";
                }
            }
            return result;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}/{type}")]
        public string Get(int id, DataType type) => _schedulerService.ShowPayment(id, type);

        // POST api/<ValuesController>
        [HttpPost("{type}")]
        public void Post([FromBody] ScheduledPayment payment, DataType type) => _fileManager.AddScheduledPayment(payment, type);

        // PUT api/<ValuesController>/5
        [HttpPut("{id}/{type}/{status}")]
        public void Put(int id, DataType type, bool status) => _schedulerService.ChangeStatus(id, type, status);

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}/{type}")]
        public void Delete(int id, DataType type) => _schedulerService.DeletePayment(id, type);
    }
}
