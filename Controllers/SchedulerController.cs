﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private readonly FileManager _fileManager = new FileManager();
        private readonly SchedulerService _schedulerService = new SchedulerService();
        private List<ScheduledPayment> ReadExpenses() => _fileManager.LoadScheduledPayments("MonthlyExpenses");
        private List<ScheduledPayment> ReadIncomes() => _fileManager.LoadScheduledPayments("monthlyincome");
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
                    result = result + _schedulerService.ShowPayment(x, "monthlyexpenses") + "\r\n" + "\r\n";
                }
            }
            if(incomes.Any())
            {
                for(var x = 0; x < incomes.Count; x++)
                {
                    result = result + _schedulerService.ShowPayment(x, "monthlyincome") + "\r\n" + "\r\n";
                }
            }
            return result;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}/{type}")]
        public string Get(int id, string type) => _schedulerService.ShowPayment(id, type);

        /*[HttpGet("{id}")]
        public object GetStats(int id)
        {
            return _budgetService.ShowStats(id);
        }*/

        // POST api/<ValuesController>
        [HttpPost("{type}")]
        public void Post([FromBody] ScheduledPayment payment, string type) => _fileManager.AddScheduledPayment(payment, type);

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}/{type}")]
        public void Delete(int id, string type) => _schedulerService.DeletePayment(id, type);
    }
}
